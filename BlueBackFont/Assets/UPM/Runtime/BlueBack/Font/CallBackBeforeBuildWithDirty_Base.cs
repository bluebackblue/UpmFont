

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief フォント。
*/


/** BlueBack.Font
*/
namespace BlueBack.Font
{
	/** CallBackBeforeBuildWithDirty_Base
	*/
	public interface CallBackBeforeBuildWithDirty_Base
	{
		/** [BlueBack.Font.CallBackBeforeBuildWithDirty_Base]ビルド直前。

			フラグの立っていないフォントはビルドされない。
			すべてのCallBackBeforeBuildが呼び出された後に呼び出される。

		*/
		void CallBackBeforeBuildWithDirty(bool[] a_dirtyflag);
	}
}

