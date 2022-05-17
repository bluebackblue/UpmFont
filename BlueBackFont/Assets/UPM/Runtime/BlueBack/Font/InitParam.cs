

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief 初期化パラメータ。
*/


/** BlueBack.Font
*/
namespace BlueBack.Font
{
	/** InitParam
	*/
	public struct InitParam
	{
		/** stringbuffer_capacity
		*/
		public int stringbuffer_capacity;

		/** font
		*/
		public UnityEngine.Font[] font;
		 
		/** CreateDefault
		*/
		public static InitParam CreateDefault()
		{
			return new InitParam(){
				stringbuffer_capacity = 1024,
				font = new UnityEngine.Font[0],
			};
		}
	}
}

