

/** BlueBack.Font
*/
namespace BlueBack.Font
{
	/** Device
	*/
	public sealed class Device : System.IDisposable
	{
		/** list
		*/
		public Item[] list;

		/** constructor
		*/
		public Device(Item[] a_list)
		{
			//list
			this.list = a_list;

			UnityEngine.Font.textureRebuilt += this.CallBackReBuilt;

			#if(DEF_BLUEBACK_FONT_LOG)
			DebugTool.Log("UnityEngine.Font.textureRebuilt : add");
			#endif
		}

		/** [System.IDisposable]Dispose
		*/
		public void Dispose()
		{
			UnityEngine.Font.textureRebuilt -= this.CallBackReBuilt;

			#if(DEF_BLUEBACK_FONT_LOG)
			DebugTool.Log("UnityEngine.Font.textureRebuilt : remove");
			#endif
		}

		/** CallBackReBuilt
		*/
		private void CallBackReBuilt(UnityEngine.Font a_font)
		{
			int ii_max = this.list.Length;
			for(int ii=0;ii<ii_max;ii++){
				if(this.list[ii].raw == a_font){
					this.list[ii].CallBackReBuilt();
				}
			}
		}
	}
}

