

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief フォント。
*/


/** BlueBack.Font
*/
namespace BlueBack.Font
{
	/** Key_SizeStyle
	*/
	public readonly struct Key_SizeStyle : System.IEquatable<Key_SizeStyle>
	{
		/** fontsize
		*/
		public readonly int fontsize;

		/** fontstyle
		*/
		public readonly UnityEngine.FontStyle fontstyle;

		/** constructor
		*/
		public Key_SizeStyle(int a_fontsize,UnityEngine.FontStyle a_fontstyle)
		{
			//fontsize
			this.fontsize = a_fontsize;

			//fontstyle
			this.fontstyle = a_fontstyle;
		}

		/** [System.IEquatable<T>]Equals
		*/
		public bool Equals(Key_SizeStyle a_object)
		{
			if((this.fontsize == a_object.fontsize)&&(this.fontstyle == a_object.fontstyle)){

				#if((DEF_BLUEBACK_FONT_LOG)&&(DEF_BLUEBACK_FONT_FULLDEBUG))
				DebugTool.Log("Key_SizeStyle : Equals(Key_SizeStyle) : true");
				#endif

				return true;
			}

			#if((DEF_BLUEBACK_FONT_LOG)&&(DEF_BLUEBACK_FONT_FULLDEBUG))
			DebugTool.Log("Key_SizeStyle : Equals(Key_SizeStyle) : false");
			#endif

			return false;
		}

		/** [static]==
		*/
		public static bool operator ==(Key_SizeStyle a_object_l,Key_SizeStyle a_object_r)
		{
			if((a_object_l.fontsize == a_object_r.fontsize)&&(a_object_l.fontstyle == a_object_r.fontstyle)){

				#if((DEF_BLUEBACK_FONT_LOG)&&(DEF_BLUEBACK_FONT_FULLDEBUG))
				DebugTool.Log("Key_SizeStyle : static== : true");
				#endif

				return true;
			}

			#if((DEF_BLUEBACK_FONT_LOG)&&(DEF_BLUEBACK_FONT_FULLDEBUG))
			DebugTool.Log("Key_SizeStyle : static== : false");
			#endif

			return false;
		}

		/** [static]!=
		*/
		public static bool operator !=(Key_SizeStyle a_object_l,Key_SizeStyle a_object_r)
		{
			if((a_object_l.fontsize == a_object_r.fontsize)&&(a_object_l.fontstyle == a_object_r.fontstyle)){

				#if((DEF_BLUEBACK_FONT_LOG)&&(DEF_BLUEBACK_FONT_FULLDEBUG))
				DebugTool.Log("Key_SizeStyle : static!= : false");
				#endif

				return false;
			}

			#if((DEF_BLUEBACK_FONT_LOG)&&(DEF_BLUEBACK_FONT_FULLDEBUG))
			DebugTool.Log("Key_SizeStyle : static!= : true");
			#endif

			return true;
		}

		/** GetHashCode
		*/
		public override int GetHashCode()
		{
			int t_hash = (this.fontsize,(int)this.fontstyle).GetHashCode();

			#if((DEF_BLUEBACK_FONT_LOG)&&(DEF_BLUEBACK_FONT_FULLDEBUG))
			DebugTool.Log(string.Format("Key_SizeStyle : GetHashCode : {0} : {1} : {2}",this.fontsize,this.fontstyle,t_hash));
			#endif

			return t_hash;
		}

		/** CompareTo
		*/
		public int CompareTo(Key_SizeStyle a_object)
		{
			int t_ret = (this.fontsize,(int)this.fontstyle).CompareTo((a_object.fontsize,(int)a_object.fontstyle));

			#if((DEF_BLUEBACK_FONT_LOG)&&(DEF_BLUEBACK_FONT_FULLDEBUG))
			DebugTool.Log(string.Format("Key_SizeStyle : CompareTo : {0}",t_ret));
			#endif

			return t_ret;
		}

		/** ToString
		*/
		public override string ToString()
		{
			return string.Format("<{0},{1}>",this.fontsize,this.fontstyle);
		}

		/** Equals(object)
		*/
		public override bool Equals(object a_object)
		{
			#if(DEF_BLUEBACK_FONT_ASSERT)
			DebugTool.Assert(false,"Key_SizeStyle : Equals(object)");
			#endif


			if(a_object != null){
				Key_SizeStyle t_object = (Key_SizeStyle)a_object;
				if((this.fontsize == t_object.fontsize)&&(this.fontstyle == t_object.fontstyle)){
					return true;
				}
			}

			return false;
		}
	}
}

