

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
		/** fontlist
		*/
		public FontList fontlist;

		/** callbacklist
		*/
		public CallBackList callbacklist;

		/** constructor
		*/
		public Font(in InitParam a_initparam)
		{
			//fontlist
			this.fontlist = new FontList(in a_initparam);

			//callbacklist
			this.callbacklist = new CallBackList();

			//textureRebuilt
			UnityEngine.Font.textureRebuilt += this.fontlist.CallBackChangeTexture;
		}

		/** [IDisposable]Dispose。
		*/
		public void Dispose()
		{
			//textureRebuilt
			UnityEngine.Font.textureRebuilt -= this.fontlist.CallBackChangeTexture;

			//fontlist
			this.fontlist.Dispose();
			this.fontlist = null;

			//callbacklist
			this.callbacklist.Dispose();
			this.callbacklist = null;
		}

		/** StartBuild
		*/
		public void StartBuild()
		{
			//テクスチャー再ビルド検知フラグ。
			int ii_max = this.fontlist.list.Length;
			for(int ii=0;ii<ii_max;ii++){
				this.fontlist.changetexture[ii] = false;
			}
		}

		/** EndBuild
		*/
		public void EndBuild()
		{
			//ビルド直前コールバック。
			{
				int ii_max = this.callbacklist.callback_before.Count;
				for(int ii=0;ii<ii_max;ii++){
					this.callbacklist.callback_before[ii].CallBackBeforeBuild();
				}
			}

			//文字追加コールバック。
			{
				int ii_max = this.callbacklist.callback_addstring.Count;
				for(int ii=0;ii<ii_max;ii++){
					this.callbacklist.callback_addstring[ii].CallBackAddString(this.fontlist.buildrequest);
				}
			}

			//テクスチャーが再ビルドされた場合、CallBackChangeTextureが呼び出され、changetextureフラグがtrueになる。
			{
				int ii_max = this.fontlist.list.Length;
				for(int ii=0;ii<ii_max;ii++){
					if(this.fontlist.buildrequest[ii] == true){
						this.fontlist.list[ii].Build();
					}
				}
			}

			//ＵＶ再計算コールバック。
			{
				int ii_max = this.callbacklist.callback_recalcuv.Count;
				for(int ii=0;ii<ii_max;ii++){
					this.callbacklist.callback_recalcuv[ii].CallBackReCalcUv(this.fontlist.buildrequest,this.fontlist.changetexture);
				}
			}

			//ビルドリクエスト。
			{
				int ii_max = this.fontlist.list.Length;
				for(int ii=0;ii<ii_max;ii++){
					this.fontlist.buildrequest[ii] = false;
				}
			}
		}
	}
}

