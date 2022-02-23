

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief フォント。
*/


/** BlueBack.Font
*/
namespace BlueBack.Font
{
	/** CallBackAfterBuild_Base
	*/
	public interface CallBackAfterBuild_Base
	{
		/** [BlueBack.Font.CallBackAfterBuild_Base]ビルド直後。

			a_rebultflag : ビルド完了フラグ。

		*/
		void CallBackAfterBuild(bool[] a_buildflag);
	}
}

