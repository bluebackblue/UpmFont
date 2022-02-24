

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief フォント。
*/


/** BlueBack.Font
*/
namespace BlueBack.Font
{
	/** CallBackBeforeBuildWithBuildRequest_Base
	*/
	public interface CallBackBeforeBuildWithBuildRequest_Base
	{
		/** [BlueBack.Font.CallBackBeforeBuildWithBuildRequest_Base]ビルド直前。

			すべてのCallBackBeforeBuildが呼び出された後に呼び出される。

			a_buildrequest == true : ビルドリクエストあり。

		*/
		void CallBackBeforeBuildWithBuildRequest(bool[] a_buildrequest);
	}
}

