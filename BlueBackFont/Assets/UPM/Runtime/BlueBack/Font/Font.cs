

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

		/** rebultflag
		*/
		private bool[] rebultflag;

		/** callback
		*/
		private System.Collections.Generic.List<CallBackBeforeApply_Base> callback_beforeapply;
		private System.Collections.Generic.List<CallBackAfterApply_Base> callback_afterapply;

		/** constructor
		*/
		public Font(in InitParam a_initparam)
		{
			//list
			this.list = new Item[a_initparam.font.Length];

			//rebultflag
			this.rebultflag = new bool[a_initparam.font.Length]; 

			int ii_max = a_initparam.font.Length;
			for(int ii=0;ii<ii_max;ii++){
				this.list[ii] = new Item(in a_initparam,ii);
				this.rebultflag[ii] = false;
			}

			//callback
			this.callback_beforeapply = new System.Collections.Generic.List<CallBackBeforeApply_Base>();
			this.callback_afterapply = new System.Collections.Generic.List<CallBackAfterApply_Base>();

			//rebult
			UnityEngine.Font.textureRebuilt += this.Inner_CallBackTextureRebult;
		}

		/** [IDisposable]Dispose。
		*/
		public void Dispose()
		{
			//list
			this.list = null;

			//rebultflag
			this.rebultflag = null;

			//callback
			this.callback_beforeapply = null;
			this.callback_afterapply = null;

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
					this.rebultflag[ii] = true;
				}
			}
		}

		/** コールバック。設定。
		*/
		public void SetCallBackBeforeApply(CallBackBeforeApply_Base a_callback)
		{
			this.callback_beforeapply.Add(a_callback);
		}

		/** コールバック。解除。
		*/
		public void UnSetCallBackBeforeApply(CallBackBeforeApply_Base a_callback)
		{
			this.callback_beforeapply.Remove(a_callback);
		}
		
		/** コールバック。設定。
		*/
		public void SetCallBackAfterApply(CallBackAfterApply_Base a_callback)
		{
			this.callback_afterapply.Add(a_callback);
		}

		/** コールバック。解除。
		*/
		public void UnSetCallBackAfterApply(CallBackAfterApply_Base a_callback)
		{
			this.callback_afterapply.Remove(a_callback);
		}

		/** フォント。取得。
		*/
		public UnityEngine.Font GetFont(int a_fontindex)
		{
			return this.list[a_fontindex].raw;
		}

		/** GetCharacterInfo
		*/
		public bool GetCharacterInfo(int a_fontindex,Key_CodeSizeStyle a_key,out UnityEngine.CharacterInfo a_characterinfo){
			return this.list[a_fontindex].raw.GetCharacterInfo(a_key.code,out a_characterinfo,a_key.fontsize,a_key.fontstyle);
		}

		/** AddString
		*/
		public void AddString(int a_fontindex,Key_CodeSizeStyle[] a_string)
		{
			this.list[a_fontindex].AddString(a_string);
		}

		/** CancelString
		*/
		public void CancelString(int a_fontindex)
		{
			this.list[a_fontindex].CancelString();
		}

		/** StartApply
		*/
		public void StartApply()
		{
			//直前コールバック呼び出し前にクリアする。
			int ii_max = this.list.Length;
			for(int ii=0;ii<ii_max;ii++){
				this.list[ii].texture_hashset.Clear();
			}
		}

		/** EndApply
		*/
		public void EndApply()
		{
			//直前コールバック。
			{
				int ii_max = this.callback_beforeapply.Count;
				for(int ii=0;ii<ii_max;ii++){
					this.callback_beforeapply[ii].CallBackBeforeApply();
				}
			}

			//構築。
			{
				int ii_max = this.list.Length;
				for(int ii=0;ii<ii_max;ii++){
					this.list[ii].Apply();
				}
			}

			//直後コールバック。
			{
				int ii_max = this.callback_afterapply.Count;
				for(int ii=0;ii<ii_max;ii++){
					this.callback_afterapply[ii].CallBackAfterApply(this.rebultflag);
				}
			}

			//再構築フラグ。リセット。
			{
				int ii_max = this.rebultflag.Length;
				for(int ii=0;ii<ii_max;ii++){
					this.rebultflag[ii] = false;
				}
			}
		}
	}
}

