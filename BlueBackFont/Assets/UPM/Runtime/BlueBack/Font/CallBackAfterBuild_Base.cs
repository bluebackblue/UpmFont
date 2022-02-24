

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

			a_buildrequest == true		: ビルドリクエストあり。
			a_changetexture == true		: フォントテクスチャーが再構築された。

		*/
		void CallBackAfterBuild(bool[] a_buildrequest,bool[] a_changetexture);
	}
}

