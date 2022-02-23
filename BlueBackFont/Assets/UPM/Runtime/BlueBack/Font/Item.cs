

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief フォント。
*/


/** BlueBack.Font
*/
namespace BlueBack.Font
{
	/** Item
	*/
	public class Item
	{
		/** raw
		*/
		public UnityEngine.Font raw;

		/** テクスチャーの状態。
		*/
		public System.Collections.Generic.HashSet<CharKey> texture_hashset;

		/** 追加リクエスト。
		*/
		public System.Collections.Generic.Dictionary<StringBufferKey,System.Text.StringBuilder> addrequest_stringbuffer;
		public int addrequest_capacity;

		/** dirtyflag
		*/
		public bool dirtyflag;

		/** constructor
		*/
		public Item(in InitParam a_initparam,int a_index)
		{
			//raw
			this.raw = a_initparam.font[a_index];

			//texture_hashset
			this.texture_hashset = new System.Collections.Generic.HashSet<CharKey>();

			//addrequest_stringbuffer
			this.addrequest_stringbuffer = new System.Collections.Generic.Dictionary<StringBufferKey,System.Text.StringBuilder>();
			this.addrequest_capacity = a_initparam.stringbuffer_capacity;

			//dirtyflag
			this.dirtyflag = false;
		}

		/** 使用文字列の追加。
		*/
		public void AddString(CharKey[] a_string)
		{
			int ii_max = a_string.Length;
			for(int ii=0;ii<ii_max;ii++){
				CharKey t_key = a_string[ii];
				StringBufferKey t_key_stringbuffer = new StringBufferKey(t_key.fontsize,t_key.fontstyle);

				//バッファー取得。
				System.Text.StringBuilder t_stringbuffer;
				{
					if(this.addrequest_stringbuffer.TryGetValue(t_key_stringbuffer,out t_stringbuffer) == false){
						#if(DEF_BLUEBACK_FONT_LOG)
						DebugTool.Log(string.Format("AddString : NewFontSize : {0} : {1}",t_key_stringbuffer.fontsize,t_key_stringbuffer.fontstyle));
						#endif
						t_stringbuffer = new System.Text.StringBuilder(1024);
						this.addrequest_stringbuffer.Add(t_key_stringbuffer,t_stringbuffer);
					}
				}

				if(this.texture_hashset.Add(t_key) == true){
					#if((DEF_BLUEBACK_FONT_LOG)&&(DEF_BLUEBACK_FONT_FULLDEBUG))
					DebugTool.Log(string.Format("AddString : Append : {0} : {1} : {2}",t_item.fontsize,t_item.fontstyle,t_item.code));
					#endif
					t_stringbuffer.Append(t_key.code);
					this.dirtyflag = true;
				}
			}
		}

		/** 文字列追加をキャンセル。
		*/
		public void CancelString()
		{
			this.dirtyflag = false;
			foreach(System.Collections.Generic.KeyValuePair<StringBufferKey,System.Text.StringBuilder> t_pair in this.addrequest_stringbuffer){
				t_pair.Value.Clear();
			}
		}

		/** 構築。
		*/
		public void Apply()
		{
			if(this.dirtyflag == true){
				this.dirtyflag = false;
				foreach(System.Collections.Generic.KeyValuePair<StringBufferKey,System.Text.StringBuilder> t_pair in this.addrequest_stringbuffer){
					if(t_pair.Value.Length > 0){
						string t_string = t_pair.Value.ToString();
						this.raw.RequestCharactersInTexture(t_string,t_pair.Key.fontsize,t_pair.Key.fontstyle);
						t_pair.Value.Clear();

						#if((DEF_BLUEBACK_FONT_LOG)&&(DEF_BLUEBACK_FONT_FULLDEBUG))
						DebugTool.Log(string.Format("RequestCharactersInTexture : {0} : {1} : {2} : {3} : {4}",t_pair.Key.fontsize,t_pair.Key.fontstyle,this.texture.width,this.texture.height,t_string));
						#endif
					}
				}
			}
		}
	}
}

