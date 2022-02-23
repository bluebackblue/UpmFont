

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

		/** buildflag
		*/
		private bool[] buildflag;

		/** dirtyflag
		*/
		private bool[] dirtyflag;

		/** callback
		*/
		private System.Collections.Generic.List<CallBackBeforeBuild_Base> callback_before;
		private System.Collections.Generic.List<CallBackBeforeBuildWithDirty_Base> callback_before_withdirty;
		private System.Collections.Generic.List<CallBackAfterBuild_Base> callback_after;

		/** constructor
		*/
		public Font(in InitParam a_initparam)
		{
			//list
			this.list = new Item[a_initparam.font.Length];

			//buildflag
			this.buildflag = new bool[a_initparam.font.Length]; 

			//dirtyflag
			this.dirtyflag = new bool[a_initparam.font.Length]; 

			int ii_max = a_initparam.font.Length;
			for(int ii=0;ii<ii_max;ii++){
				this.list[ii] = new Item(in a_initparam,ii);
				this.buildflag[ii] = false;
				this.dirtyflag[ii] = false;
			}

			//callback
			this.callback_before = new System.Collections.Generic.List<CallBackBeforeBuild_Base>();
			this.callback_before_withdirty = new System.Collections.Generic.List<CallBackBeforeBuildWithDirty_Base>();
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

			//buildflag
			this.buildflag = null;

			//dirtyflag
			this.dirtyflag = null;

			//callback
			this.callback_before = null;
			this.callback_before_withdirty = null;
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
					this.buildflag[ii] = true;
				}
			}
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
		public void SetCallBackBeforeBuildWithDirty(CallBackBeforeBuildWithDirty_Base a_callback)
		{
			this.callback_before_withdirty.Add(a_callback);
		}

		/** コールバック。解除。
		*/
		public void UnSetCallBackBeforeBuildWithDirty(CallBackBeforeBuildWithDirty_Base a_callback)
		{
			this.callback_before_withdirty.Remove(a_callback);
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
			this.list[a_fontindex].AddString(a_string);
		}

		/** CancelString
		*/
		public void CancelString(int a_fontindex)
		{
			this.list[a_fontindex].CancelString();
		}

		/** SetDirty
		*/
		public void SetDirty(int a_fontindex)
		{
			this.list[a_fontindex].dirtyflag = true;
		}

		/** StartBuild
		*/
		public void StartBuild()
		{
			//直前コールバック呼び出し前にクリアする。
			int ii_max = this.list.Length;
			for(int ii=0;ii<ii_max;ii++){
				this.list[ii].texture_hashset.Clear();
			}
		}

		/** EndBuild
		*/
		public void EndBuild()
		{
			//ビルド前。
			{
				int ii_max = this.callback_before.Count;
				for(int ii=0;ii<ii_max;ii++){
					this.callback_before[ii].CallBackBeforeBuild();
				}
			}

			//dirtyflag
			{
				int ii_max = this.list.Length;
				for(int ii=0;ii<ii_max;ii++){
					this.dirtyflag[ii] = this.list[ii].dirtyflag;
				}
			}

			//ビルド前。フラグ集計後。
			{
				int ii_max = this.callback_before_withdirty.Count;
				for(int ii=0;ii<ii_max;ii++){
					this.callback_before_withdirty[ii].CallBackBeforeBuildWithDirty(this.dirtyflag);
				}
			}

			//Build
			{
				int ii_max = this.list.Length;
				for(int ii=0;ii<ii_max;ii++){
					if(this.dirtyflag[ii] == true){
						this.list[ii].Build();
					}else{
						#if(DEF_BLUEBACK_FONT_ASSERT)
						DebugTool.Assert(this.list[ii].dirtyflag == false,"error");
						#endif

					}
				}
			}

			//CallBackAfterBuild
			{
				int ii_max = this.callback_after.Count;
				for(int ii=0;ii<ii_max;ii++){
					this.callback_after[ii].CallBackAfterBuild(this.buildflag);
				}
			}

			//フラグリセット。
			{
				int ii_max = this.buildflag.Length;
				for(int ii=0;ii<ii_max;ii++){
					this.buildflag[ii] = false;
				}
			}
		}
	}
}

