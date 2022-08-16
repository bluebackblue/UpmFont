

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
	public sealed class Item : System.IDisposable
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

		/** constructor
		*/
		public Item(in InitParam a_initparam,int a_index)
		{
			//raw
			this.raw = a_initparam.font[a_index];

			//texture
			this.texture_hashset = new System.Collections.Generic.HashSet<CharKey>();

			//addrequest
			this.addrequest_stringbuffer = new System.Collections.Generic.Dictionary<StringBufferKey,System.Text.StringBuilder>();
			this.addrequest_capacity = a_initparam.stringbuffer_capacity;
		}

		/** [IDisposable]Dispose。
		*/
		public void Dispose()
		{
			//raw
			this.raw = null;

			//texture
			this.texture_hashset = null;

			//addrequest
			this.addrequest_stringbuffer = null;
		}

		/** クリア。
		*/
		public void ClearHashSet()
		{
			this.texture_hashset.Clear();
		}

		/** クリア。
		*/
		public void ClearStringBuilder()
		{
			foreach(System.Collections.Generic.KeyValuePair<StringBufferKey,System.Text.StringBuilder> t_pair in this.addrequest_stringbuffer){
				t_pair.Value.Clear();
			}
		}

		/** 使用文字列の追加。

			return == true : 追加あり。

		*/
		public bool AddString(CharKey[] a_string)
		{
			bool t_change = false;

			int ii_max = a_string.Length;
			for(int ii=0;ii<ii_max;ii++){
				CharKey t_key = a_string[ii];
				StringBufferKey t_key_stringbuffer = new StringBufferKey(t_key.fontsize,t_key.fontstyle);

				//バッファー取得。
				System.Text.StringBuilder t_stringbuffer;
				{
					if(this.addrequest_stringbuffer.TryGetValue(t_key_stringbuffer,out t_stringbuffer) == false){
						#if(DEF_BLUEBACK_DEBUG_LOG)
						DebugTool.Log(string.Format("AddString : NewFontSize : {0} : {1} : {2}",t_key.fontsize,t_key.fontstyle,t_key.code));
						#endif
						t_stringbuffer = new System.Text.StringBuilder(this.addrequest_capacity);
						this.addrequest_stringbuffer.Add(t_key_stringbuffer,t_stringbuffer);
					}
				}

				if(this.texture_hashset.Add(t_key) == true){
					#if(DEF_BLUEBACK_DEBUG_LOG)
					DebugTool.Log(string.Format("AddString : Append : {0} : {1} : {2}",t_key.fontsize,t_key.fontstyle,t_key.code));
					#endif
					t_stringbuffer.Append(t_key.code);
					t_change = true;
				}
			}

			return t_change;
		}

		/** ビルド。
		*/
		public void Build()
		{
			foreach(System.Collections.Generic.KeyValuePair<StringBufferKey,System.Text.StringBuilder> t_pair in this.addrequest_stringbuffer){
				if(t_pair.Value.Length > 0){
					string t_string = t_pair.Value.ToString();
					this.raw.RequestCharactersInTexture(t_string,t_pair.Key.fontsize,t_pair.Key.fontstyle);
					t_pair.Value.Clear();

					#if(DEF_BLUEBACK_DEBUG_LOG)
					DebugTool.Log(string.Format("RequestCharactersInTexture : {0} : {1} : {2} : {3} : {4}",t_pair.Key.fontsize,t_pair.Key.fontstyle,this.raw.material.mainTexture.width,this.raw.material.mainTexture.height,t_string));
					#endif
				}
			}
		}
	}
}

