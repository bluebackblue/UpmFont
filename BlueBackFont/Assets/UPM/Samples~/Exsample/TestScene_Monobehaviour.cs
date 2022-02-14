

/** Samples.Font.Exsample
*/
namespace Samples.Font.Exsample
{
	/** TestScene_Monobehaviour
	*/
	public sealed class TestScene_Monobehaviour : UnityEngine.MonoBehaviour
	{
		/** gl
		*/
		private BlueBack.Gl.Gl gl;

		/** VIRTUAL_SCREEN
		*/
		private const int VIRTUAL_SCREEN_W = 1280;
		private const int VIRTUAL_SCREEN_H = 720;

		/** screenparam
		*/
		public BlueBack.Gl.ScreenParam screenparam;

		/** rendertexture
		*/
		public int rendertexture_w;
		public int rendertexture_h;

		/** Awake
		*/
		public void Awake()
		{
			//rendertexture
			this.rendertexture_w = UnityEngine.Screen.width;
			this.rendertexture_h = UnityEngine.Screen.height;

			//screenparam
			this.screenparam = BlueBack.Gl.ScreenTool.CreateScreenParamWidthStretch(VIRTUAL_SCREEN_W,VIRTUAL_SCREEN_H,this.rendertexture_w,this.rendertexture_h);

			//gl
			{
				BlueBack.Gl.InitParam t_initparam = BlueBack.Gl.InitParam.CreateDefault();
				{
					t_initparam.spritelist_max = 2;
					t_initparam.texture_max = 2;
					t_initparam.material_max = 2;
					t_initparam.sprite_max = 100;
				}
				this.gl = new BlueBack.Gl.Gl(in t_initparam);

				//SetScreenParam
				#if(DEF_BLUEBACK_GL_DEBUGVIEW)
				BlueBack.Gl.Sprite_DebugView_MonoBehaviour.SetScreenParam(in this.screenparam);
				#endif
					
				//materialexecutelist
				this.gl.materialexecutelist.list[0] = new BlueBack.Gl.MaterialExecute_SImple(this.gl,UnityEngine.Resources.Load<UnityEngine.Material>("opaque"));
				this.gl.materialexecutelist.list[1] = new BlueBack.Gl.MaterialExecute_SImple(this.gl,UnityEngine.Resources.Load<UnityEngine.Material>("transparent"));
			}
		}

		/** Update
		*/
		public void Update()
		{
		}

		/** OnDestroy
		*/
		public void OnDestroy()
		{
			if(this.gl != null){
				this.gl.Dispose();
				this.gl = null;
			}
		}
	}
}

