

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief フォント。
*/


/** BlueBack.Font
*/
namespace BlueBack.Font
{
	/** CharKey
	*/
	public readonly struct CharKey : System.IEquatable<CharKey>
	{
		/** code
		*/
		public readonly char code;

		/** fontsize
		*/
		public readonly int fontsize;

		/** fontstyle
		*/
		public readonly UnityEngine.FontStyle fontstyle;

		/** constructor
		*/
		public CharKey(char a_code,int a_fontsize,UnityEngine.FontStyle a_fontstyle)
		{
			//code
			this.code = a_code;

			//fontsize
			this.fontsize = a_fontsize;

			//fontstyle
			this.fontstyle = a_fontstyle;
		}

		/** [System.IEquatable<T>]Equals
		*/
		public bool Equals(CharKey a_object)
		{
			if((this.code == a_object.code)&&(this.fontsize == a_object.fontsize)&&(this.fontstyle == a_object.fontstyle)){

				#if(DEF_BLUEBACK_DEBUG_DETAIL)
				DebugTool.Detail("CharKey : Equals(CharKey) : true");
				#endif

				return true;
			}

			#if(DEF_BLUEBACK_DEBUG_DETAIL)
			DebugTool.Detail("CharKey : Equals(CharKey) : false");
			#endif

			return false;
		}

		/** [static]==
		*/
		public static bool operator ==(CharKey a_object_l,CharKey a_object_r)
		{
			if((a_object_l.code == a_object_r.code)&&(a_object_l.fontsize == a_object_r.fontsize)&&(a_object_l.fontstyle == a_object_r.fontstyle)){

				#if(DEF_BLUEBACK_DEBUG_DETAIL)
				DebugTool.Detail("CharKey : static== : true");
				#endif

				return true;
			}

			#if(DEF_BLUEBACK_DEBUG_DETAIL)
			DebugTool.Detail("CharKey : static== : false");
			#endif

			return false;
		}

		/** [static]!=
		*/
		public static bool operator !=(CharKey a_object_l,CharKey a_object_r)
		{
			if((a_object_l.code == a_object_r.code)&&(a_object_l.fontsize == a_object_r.fontsize)&&(a_object_l.fontstyle == a_object_r.fontstyle)){

				#if(DEF_BLUEBACK_DEBUG_DETAIL)
				DebugTool.Detail("CharKey : static!= : false");
				#endif

				return false;
			}

			#if(DEF_BLUEBACK_DEBUG_DETAIL)
			DebugTool.Detail("CharKey : static!= : true");
			#endif

			return true;
		}

		/** GetHashCode
		*/
		public override int GetHashCode()
		{
			int t_hash = (this.code,this.fontsize,(int)this.fontstyle).GetHashCode();

			#if(DEF_BLUEBACK_DEBUG_DETAIL)
			DebugTool.Detail(string.Format("CharKey : GetHashCode : {0} : {1} : {2} : {3}",this.code,this.fontsize,this.fontstyle,t_hash));
			#endif

			return t_hash;
		}

		/** CompareTo
		*/
		public int CompareTo(CharKey a_object)
		{
			int t_ret = (this.code,this.fontsize,(int)this.fontstyle).CompareTo((a_object.code,a_object.fontsize,(int)a_object.fontstyle));

			#if(DEF_BLUEBACK_DEBUG_DETAIL)
			DebugTool.Detail(string.Format("CharKey : CompareTo : {0}",t_ret));
			#endif

			return t_ret;
		}

		/** ToString
		*/
		public override string ToString()
		{
			return string.Format("<{0},{1},{2}>",this.code,this.fontsize,this.fontstyle);
		}

		/** Equals(object)
		*/
		public override bool Equals(object a_object)
		{
			if(a_object != null){
				CharKey t_object = (CharKey)a_object;
				if((this.code == t_object.code)&&(this.fontsize == t_object.fontsize)&&(this.fontstyle == t_object.fontstyle)){

					#if(DEF_BLUEBACK_DEBUG_ASSERT)
					DebugTool.Assert(false,"CharKey : Equals(object) : true");
					#endif

					return true;
				}
			}

			#if(DEF_BLUEBACK_DEBUG_ASSERT)
			DebugTool.Assert(false,"CharKey : Equals(object) : false");
			#endif

			return false;
		}
	}
}

