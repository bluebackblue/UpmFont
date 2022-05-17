

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief フォント。
*/


/** BlueBack.Font
*/
namespace BlueBack.Font
{
	/** FontList
	*/
	public sealed class FontList : System.IDisposable
	{
		/** list
		*/
		public Item[] list;

		/** changetexture
		*/
		public bool[] changetexture;

		/** buildrequest
		*/
		public bool[] buildrequest;

		/** constructor
		*/
		public FontList(in InitParam a_initparam)
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
		}

		/** [IDisposable]Dispose。
		*/
		public void Dispose()
		{
			//list
			if(this.list != null){
				int ii_max = this.list.Length;
				for(int ii=0;ii<ii_max;ii++){
					this.list[ii].Dispose();
				}
				this.list = null;
			}

			//changetexture
			this.changetexture = null;

			//buildrequest
			this.buildrequest = null;
		}

		/** CallBackChangeTexture
		*/
		public void CallBackChangeTexture(UnityEngine.Font a_font)
		{
			int ii_max = this.list.Length;
			for(int ii=0;ii<ii_max;ii++){
				if(this.list[ii].raw == a_font){
					this.changetexture[ii] = true;
				}
			}
		}

		/** フォント。取得。
		*/
		public UnityEngine.Font GetFont(int a_fontindex)
		{
			return this.list[a_fontindex].raw;
		}

		/** 文字情報。取得。
		*/
		public bool GetCharacterInfo(int a_fontindex,CharKey a_key,out UnityEngine.CharacterInfo a_characterinfo)
		{
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
	}
}

