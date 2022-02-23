

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
	public class Item : CallBackReBuilt_Base
	{
		/** raw
		*/
		public UnityEngine.Font raw;

		/** index
		*/
		public int index;

		/** texture
		*/
		public UnityEngine.Texture texture;

		/** テクスチャーの状態。
		*/
		public System.Collections.Generic.HashSet<Key_CodeSizeStyle> texture_hashset;

		/** 追加リクエスト。
		*/
		public System.Collections.Generic.Dictionary<Key_SizeStyle,StringBufferItem> addrequest_stringbuffer;
		public int addrequest_capacity;

		/** callback
		*/
		public System.Collections.Generic.List<CallBackBeforeApply_Base> callback_beforeapply;
		public System.Collections.Generic.List<CallBackAfterApply_Base> callback_afterapply;

		/** constructor
		*/
		public Item(in InitParam a_initparam,int a_index)
		{
			//raw
			this.raw = a_initparam.font[a_index];

			//texture
			this.texture = this.raw.material.mainTexture;

			//texture_hashset
			this.texture_hashset = new System.Collections.Generic.HashSet<Key_CodeSizeStyle>();

			//addrequest_stringbuffer
			this.addrequest_stringbuffer = new System.Collections.Generic.Dictionary<Key_SizeStyle,StringBufferItem>();
			this.addrequest_capacity = a_initparam.stringbuffer_capacity;

			//callback
			this.callback_beforeapply = new System.Collections.Generic.List<CallBackBeforeApply_Base>();
			this.callback_afterapply = new System.Collections.Generic.List<CallBackAfterApply_Base>();
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

		/** 使用文字列の追加。
		*/
		public void AddString(string a_string,int a_fontsize,UnityEngine.FontStyle a_fontstyle)
		{
			Key_SizeStyle t_key = new Key_SizeStyle(a_fontsize,a_fontstyle);

			StringBufferItem t_stringbufferitem;
			if(this.addrequest_stringbuffer.TryGetValue(t_key,out t_stringbufferitem) == false){
				#if(DEF_BLUEBACK_FONT_LOG)
				DebugTool.Log(string.Format("AddString : NewFontSize : {0} : {1}",t_key.fontsize,t_key.fontstyle));
				#endif
				
				t_stringbufferitem = new StringBufferItem(in t_key,1024);
				this.addrequest_stringbuffer.Add(t_key,t_stringbufferitem);
			}

			for(int ii=0;ii<a_string.Length;ii++){
				Key_CodeSizeStyle t_item = new Key_CodeSizeStyle(a_string[ii],t_key.fontsize,t_key.fontstyle);
				if(this.texture_hashset.Add(t_item) == true){
					#if(DEF_BLUEBACK_FONT_LOG) && DEF_BLUEBACK_FONT_FULLDEBUG
					DebugTool.Log(string.Format("AddString : Append : {0} : {1} : {2}",t_item.fontsize,t_item.fontstyle,t_item.code));
					#endif

					t_stringbufferitem.stringbuffer.Append(t_item.code);
				}
			}
		}

		/** 構築。
		*/
		public void Apply()
		{
			//直前コールバック。
			{
				int ii_max = this.callback_beforeapply.Count;
				for(int ii=0;ii<ii_max;ii++){
					this.callback_beforeapply[ii].CallBackBeforeApply(this.index);
				}
			}

			foreach(System.Collections.Generic.KeyValuePair<Key_SizeStyle,StringBufferItem> t_pair in this.addrequest_stringbuffer){
				if(t_pair.Value.stringbuffer.Length > 0){
					string t_string = t_pair.Value.stringbuffer.ToString();
					this.raw.RequestCharactersInTexture(t_string,t_pair.Key.fontsize,t_pair.Key.fontstyle);
					t_pair.Value.stringbuffer.Clear();

					#if(DEF_BLUEBACK_FONT_LOG) && DEF_BLUEBACK_FONT_FULLDEBUG
					DebugTool.Log(string.Format("RequestCharactersInTexture : {0} : {1} : {2} : {3} : {4}",t_pair.Key.fontsize,t_pair.Key.fontstyle,this.texture.width,this.texture.height,t_string));
					#endif
				}
			}

			//直後コールバック。
			{
				int ii_max = this.callback_afterapply.Count;
				for(int ii=0;ii<ii_max;ii++){
					this.callback_afterapply[ii].CallBackAfterApply(this.index);
				}
			}
		}

		/** テクスチャーの状態。クリア。
		*/
		public void ClearTextureHashSet()
		{
			this.texture_hashset.Clear();
		}

		/** ClearStringBuffer
		*/
		public void ClearStringBuffer()
		{
			foreach(System.Collections.Generic.KeyValuePair<Key_SizeStyle,StringBufferItem> t_pair in this.addrequest_stringbuffer){
				t_pair.Value.stringbuffer.Clear();
			}
		}

		/** [BlueBack.Font.CallBackReBuilt_Base]テクスチャ再構築直後。
		*/
		public void CallBackReBuilt()
		{
			#if(DEF_BLUEBACK_FONT_LOG)
			DebugTool.Log("CallBackReBuilt");
			#endif
		}
	}
}

