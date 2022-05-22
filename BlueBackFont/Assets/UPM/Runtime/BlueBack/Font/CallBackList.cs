

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief フォント。
*/


/** BlueBack.Font
*/
namespace BlueBack.Font
{
	/** CallBackList
	*/
	public sealed class CallBackList : System.IDisposable
	{
		/** callback
		*/
		public System.Collections.Generic.List<CallBackBeforeBuild_Base> callback_before;
		public System.Collections.Generic.List<CallBackAddString_Base> callback_addstring;
		public System.Collections.Generic.List<CallBackReCalcUv_Base> callback_recalcuv;

		/** constructor
		*/
		public CallBackList()
		{
			//callback
			this.callback_before = new System.Collections.Generic.List<CallBackBeforeBuild_Base>();
			this.callback_addstring = new System.Collections.Generic.List<CallBackAddString_Base>();
			this.callback_recalcuv = new System.Collections.Generic.List<CallBackReCalcUv_Base>();

		}

		/** [IDisposable]Dispose。
		*/
		public void Dispose()
		{
			//callback
			this.callback_before = null;
			this.callback_addstring = null;
			this.callback_recalcuv = null;

		}

		/** ビルド直前コールバック。設定。
		*/
		public void SetCallBackBeforeBuild(CallBackBeforeBuild_Base a_callback)
		{
			this.callback_before.Add(a_callback);
		}

		/** ビルド直前コールバック。解除。
		*/
		public void UnSetCallBackBeforeBuild(CallBackBeforeBuild_Base a_callback)
		{
			this.callback_before.Remove(a_callback);
		}

		/** 文字追加コールバック。設定。
		*/
		public void SetCallBackAddString(CallBackAddString_Base a_callback)
		{
			this.callback_addstring.Add(a_callback);
		}

		/** 文字追加コールバック。解除。
		*/
		public void UnSetCallBackAddString(CallBackAddString_Base a_callback)
		{
			this.callback_addstring.Remove(a_callback);
		}

		/** ＵＶ再計算コールバック。設定。
		*/
		public void SetCallBackReCalcUv(CallBackReCalcUv_Base a_callback)
		{
			this.callback_recalcuv.Add(a_callback);
		}

		/** ＵＶ再計算コールバック。解除。
		*/
		public void UnSetCallBackReCalcUv(CallBackReCalcUv_Base a_callback)
		{
			this.callback_recalcuv.Remove(a_callback);
		}
	}
}

