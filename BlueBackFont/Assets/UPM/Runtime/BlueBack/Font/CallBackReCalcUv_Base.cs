

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief フォント。コールバック。
*/


/** BlueBack.Font
*/
namespace BlueBack.Font
{
	/** CallBackReCalcUv_Base
	*/
	public interface CallBackReCalcUv_Base
	{
		/** [BlueBack.Font.CallBackReCalcUv_Base]ＵＶ再計算コールバック。

			a_buildrequest == true		: ビルドリクエストあり。
			a_changetexture == true		: フォントテクスチャーが再構築された。

		*/
		void CallBackReCalcUv(bool[] a_buildrequest,bool[] a_changetexture);
	}
}

