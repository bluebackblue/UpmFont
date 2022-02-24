

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief フォント。
*/


/** BlueBack.Font
*/
namespace BlueBack.Font
{
	/** Font
	*/
	public sealed class Font : System.IDisposable
	{
		/** list
		*/
		private Item[] list;

		/** changetexture
		*/
		private bool[] changetexture;

		/** buildrequest
		*/
		private bool[] buildrequest;

		/** callback
		*/
		private System.Collections.Generic.List<CallBackBeforeBuild_Base> callback_before;
		private System.Collections.Generic.List<CallBackBeforeBuildWithBuildRequest_Base> callback_before_with_buildrequest;
		private System.Collections.Generic.List<CallBackAfterBuild_Base> callback_after;

		/** constructor
		*/
		public Font(in InitParam a_initparam)
		{
			//list
			this.list = new Item[a_initparam.font.Length];

			//changetexture
			this.changetexture = new bool[a_initparam.font.Length]; 

			//buildrequest
			this.buildrequest = new bool[a_initparam.font.Length]; 

			int ii_max = a_initparam.font.Length;
			for(int ii=0;ii<ii_max;ii++){
				this.list[ii] = new Item(in a_initparam,ii);
				this.changetexture[ii] = false;
				this.buildrequest[ii] = false;
			}

			//callback
			this.callback_before = new System.Collections.Generic.List<CallBackBeforeBuild_Base>();
			this.callback_before_with_buildrequest = new System.Collections.Generic.List<CallBackBeforeBuildWithBuildRequest_Base>();
			this.callback_after = new System.Collections.Generic.List<CallBackAfterBuild_Base>();

			//rebult
			UnityEngine.Font.textureRebuilt += this.Inner_CallBackTextureRebult;
		}

		/** [IDisposable]Dispose。
		*/
		public void Dispose()
		{
			//list
			this.list = null;

			//changetexture
			this.changetexture = null;

			//buildrequest
			this.buildrequest = null;

			//callback
			this.callback_before = null;
			this.callback_before_with_buildrequest = null;
			this.callback_after = null;

			//rebult
			UnityEngine.Font.textureRebuilt -= this.Inner_CallBackTextureRebult;
		}

		/** Inner_CallBackTextureRebult
		*/
		private void Inner_CallBackTextureRebult(UnityEngine.Font a_font)
		{
			int ii_max = this.list.Length;
			for(int ii=0;ii<ii_max;ii++){
				if(this.list[ii].raw == a_font){
					this.changetexture[ii] = true;
				}
			}
		}

		/** SetBuildRequest
		*/
		public void SetBuildRequest(int a_fontindex,bool a_flag)
		{
			this.buildrequest[a_fontindex] = a_flag;
		}

		/** コールバック。設定。
		*/
		public void SetCallBackBeforeBuild(CallBackBeforeBuild_Base a_callback)
		{
			this.callback_before.Add(a_callback);
		}

		/** コールバック。解除。
		*/
		public void UnSetCallBackBeforeBuild(CallBackBeforeBuild_Base a_callback)
		{
			this.callback_before.Remove(a_callback);
		}

		/** コールバック。設定。
		*/
		public void SetCallBackBeforeBuildWithBuildRequest(CallBackBeforeBuildWithBuildRequest_Base a_callback)
		{
			this.callback_before_with_buildrequest.Add(a_callback);
		}

		/** コールバック。解除。
		*/
		public void UnSetCallBackBeforeBuildWithBuildRequest(CallBackBeforeBuildWithBuildRequest_Base a_callback)
		{
			this.callback_before_with_buildrequest.Remove(a_callback);
		}
		
		/** コールバック。設定。
		*/
		public void SetCallBackAfterBuild(CallBackAfterBuild_Base a_callback)
		{
			this.callback_after.Add(a_callback);
		}

		/** コールバック。解除。
		*/
		public void UnSetCallBackAfterBuild(CallBackAfterBuild_Base a_callback)
		{
			this.callback_after.Remove(a_callback);
		}

		/** フォント。取得。
		*/
		public UnityEngine.Font GetFont(int a_fontindex)
		{
			return this.list[a_fontindex].raw;
		}

		/** GetCharacterInfo
		*/
		public bool GetCharacterInfo(int a_fontindex,CharKey a_key,out UnityEngine.CharacterInfo a_characterinfo){
			return this.list[a_fontindex].raw.GetCharacterInfo(a_key.code,out a_characterinfo,a_key.fontsize,a_key.fontstyle);
		}

		/** AddString
		*/
		public void AddString(int a_fontindex,CharKey[] a_string)
		{
			if(this.list[a_fontindex].AddString(a_string) == true){
				this.buildrequest[a_fontindex] = true;
			}
		}

		/** CancelString
		*/
		public void CancelString(int a_fontindex)
		{
			this.list[a_fontindex].CancelString();
			this.buildrequest[a_fontindex] = false;
		}

		/** StartBuild
		*/
		public void StartBuild()
		{
			//直前コールバック呼び出し前にクリアする。
			{
				int ii_max = this.list.Length;
				for(int ii=0;ii<ii_max;ii++){
					this.list[ii].texture_hashset.Clear();
				}
			}

			//フラグリセット。
			{
				int ii_max = this.changetexture.Length;
				for(int ii=0;ii<ii_max;ii++){
					this.changetexture[ii] = false;
				}
			}
		}

		/** EndBuild
		*/
		public void EndBuild()
		{
			//ビルド直前。
			{
				int ii_max = this.callback_before.Count;
				for(int ii=0;ii<ii_max;ii++){
					this.callback_before[ii].CallBackBeforeBuild();
				}
			}

			//ビルド直前。
			{
				int ii_max = this.callback_before_with_buildrequest.Count;
				for(int ii=0;ii<ii_max;ii++){
					this.callback_before_with_buildrequest[ii].CallBackBeforeBuildWithBuildRequest(this.buildrequest);
				}
			}

			//Build
			{
				int ii_max = this.list.Length;
				for(int ii=0;ii<ii_max;ii++){
					if(this.buildrequest[ii] == true){
						this.list[ii].Build();
					}
				}
			}

			//ビルド直後。
			{
				int ii_max = this.callback_after.Count;
				for(int ii=0;ii<ii_max;ii++){
					this.callback_after[ii].CallBackAfterBuild(this.buildrequest,this.changetexture);
				}
			}
		}
	}
}

