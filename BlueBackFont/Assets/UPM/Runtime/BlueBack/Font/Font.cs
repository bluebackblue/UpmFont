

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

		/** fontlist
		*/
		public Item[] fontlist;

		/** constructor
		*/
		public Font(in InitParam a_initparam)
		{
			//fontlist
			this.fontlist = new Item[a_initparam.font.Length];
			int ii_max = this.fontlist.Length;
			for(int ii=0;ii<ii_max;ii++){
				this.fontlist[ii] = new Item(in a_initparam,ii);
			}

			//device
			this.device = new Device(this.fontlist);
		}

		/** [IDisposable]Dispose。
		*/
		public void Dispose()
		{
			//fontlist
			this.fontlist = null;

			//device
			if(this.device != null){
				this.device.Dispose();
				this.device = null;
			}
		}
	}
}

