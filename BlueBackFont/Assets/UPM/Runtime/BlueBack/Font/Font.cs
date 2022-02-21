

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief フォント。
*/


/** BlueBack.Font
*/
namespace BlueBack.Font
{
	/** Font
	*/
	public sealed class Font : System.IDisposable
	{
		/** device
		*/
		private Device device;

		/** list
		*/
		public Item[] list;

		/** constructor
		*/
		public Font(in InitParam a_initparam)
		{
			//list
			this.list = new Item[a_initparam.font.Length];
			int ii_max = this.list.Length;
			for(int ii=0;ii<ii_max;ii++){
				this.list[ii] = new Item(in a_initparam,ii);
			}

			//device
			this.device = new Device(this.list);
		}

		/** [IDisposable]Dispose。
		*/
		public void Dispose()
		{
			//list
			this.list = null;

			//device
			if(this.device != null){
				this.device.Dispose();
				this.device = null;
			}
		}
	}
}

