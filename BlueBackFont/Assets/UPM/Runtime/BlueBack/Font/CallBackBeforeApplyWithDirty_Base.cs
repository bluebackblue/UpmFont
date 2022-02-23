

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief フォント。
*/


/** BlueBack.Font
*/
namespace BlueBack.Font
{
	/** CallBackBeforeApplyWithDirty_Base
	*/
	public interface CallBackBeforeApplyWithDirty_Base
	{
		/** [BlueBack.Font.CallBackBeforeApplyWithDirty_Base]構築直前。

			a_dirtyflag				: フラグの立っていないフォントは再構築されない。

			すべてのCallBackBeforeApplyが呼び出された後に呼び出される。

		*/
		void CallBackBeforeApplyWithDirty(bool[] a_dirtyflag);
	}
}

