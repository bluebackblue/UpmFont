

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief フォント。コールバック。
*/


/** BlueBack.Font
*/
namespace BlueBack.Font
{
	/** CallBackAddString_Base
	*/
	public interface CallBackAddString_Base
	{
		/** [BlueBack.Font.CallBackAddString_Base]文字追加コールバック。

			a_buildrequest == true : ビルドリクエストあり。

		*/
		void CallBackAddString(bool[] a_buildrequest);
	}
}

