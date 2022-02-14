using UnityEngine;

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * @brief フォント。
*/


/** BlueBack.Font
*/
namespace BlueBack.Font
{
	/** StringBufferItem
	*/
	public class StringBufferItem
	{
		/** key
		*/
		public Key_SizeStyle key;

		/** stringbuffer
		*/
		public System.Text.StringBuilder stringbuffer;

		/** constructor
		*/
		public StringBufferItem(in Key_SizeStyle a_key,int a_capacity)
		{
			this.key = a_key;
			this.stringbuffer = new System.Text.StringBuilder(a_capacity);
		}
	}
}

