

/** Samples.Font.Exsample
*/
namespace Samples.Font.Exsample
{
	/** TestScene_Monobehaviour
	*/
	public sealed class TestScene_Monobehaviour : UnityEngine.MonoBehaviour
	{
		/** font
		*/
		public BlueBack.Font.Font font;

		/** FONTSIZE
		*/
		private const int FONTSIZE = 32;

		/** gl
		*/
		public BlueBack.Gl.Gl gl;

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

		/** textsprite
		*/
		public TextSprite textsprite1;
		public TextSprite textsprite2;

		/** TextSprite
		*/
		public sealed class TextSprite : BlueBack.Font.CallBackBeforeApply_Base , BlueBack.Font.CallBackAfterApply_Base , System.IDisposable
		{
			/** monobehaviour
			*/
			private TestScene_Monobehaviour monobehaviour;

			/** text
			*/
			private string text;

			/** spriteindex
			*/
			private BlueBack.Gl.SpriteIndex[] spriteindex;

			/** xy
			*/
			private int x;
			private int y;

			/** constructor
			*/
			public TextSprite(string a_text,int a_x,int a_y,TestScene_Monobehaviour a_monobehaviour)
			{
				BlueBack.Font.Item t_font = a_monobehaviour.font.list[0];

				this.text = a_text;
				t_font.SetCallBackBeforeApply(this);
				t_font.SetCallBackAfterApply(this);
				this.monobehaviour = a_monobehaviour;
				this.spriteindex = new BlueBack.Gl.SpriteIndex[a_text.Length];
				for(int ii=0;ii<this.spriteindex.Length;ii++){
					this.spriteindex[ii] = a_monobehaviour.gl.spritelist[0].CreateSprite();
				}
				this.x = a_x;
				this.y = a_y;
			}

			/** [System.IDisposable]破棄。
			*/
			public void Dispose()
			{
				BlueBack.Font.Item t_font = this.monobehaviour.font.list[0];

				t_font.UnSetCallBackBeforeApply(this);
				t_font.UnSetCallBackAfterApply(this);

				for(int ii=0;ii<this.spriteindex.Length;ii++){
					this.spriteindex[ii].Dispose();
				}
			}

			/** [BlueBack.Font.CallBackPreApply_Base]構築直前。
			*/
			public void CallBackBeforeApply()
			{
				BlueBack.Font.Item t_font = this.monobehaviour.font.list[0];

				t_font.AddString(this.text.ToString(),FONTSIZE,UnityEngine.FontStyle.Normal);
			}

			/** [BlueBack.Font.CallBackAfterApply_Base]構築直後。
			*/
			public void CallBackAfterApply()
			{
				BlueBack.Font.Item t_font = this.monobehaviour.font.list[0];

				int t_x = this.x;
				int t_y = this.y;

				for(int ii=0;ii<this.text.Length;ii++){
					ref BlueBack.Gl.SpriteBuffer t_spritebuffer = ref this.spriteindex[ii].GetSpriteBuffer();
					UnityEngine.CharacterInfo t_characterinfo;
					if(t_font.font.GetCharacterInfo(this.text[ii],out t_characterinfo,FONTSIZE,UnityEngine.FontStyle.Normal) == true){

						t_spritebuffer.visible = true;
						t_spritebuffer.material_index = 0;
						t_spritebuffer.texture_index = 0;
						t_spritebuffer.color = new UnityEngine.Color(1.0f,1.0f,1.0f,1.0f);

						t_spritebuffer.texcord = Unity.Mathematics.math.float2x4(
							Unity.Mathematics.math.float2(t_characterinfo.uvTopLeft.x,t_characterinfo.uvTopLeft.y),
							Unity.Mathematics.math.float2(t_characterinfo.uvTopRight.x,t_characterinfo.uvTopRight.y),
							Unity.Mathematics.math.float2(t_characterinfo.uvBottomRight.x,t_characterinfo.uvBottomRight.y),
							Unity.Mathematics.math.float2(t_characterinfo.uvBottomLeft.x,t_characterinfo.uvBottomLeft.y)
						);

						BlueBack.Gl.SpriteTool.SetVertex(
							ref t_spritebuffer,
							Unity.Mathematics.math.float2x2(
								Unity.Mathematics.math.float2(t_characterinfo.minX,- t_characterinfo.maxY),
								Unity.Mathematics.math.float2(t_characterinfo.maxX,- t_characterinfo.minY)
							),
							Unity.Mathematics.math.float2(t_x,t_y),
							in this.spriteindex[ii].spritelist.gl.screenparam
						);

						t_x += t_characterinfo.advance;

						if(t_x + FONTSIZE >= VIRTUAL_SCREEN_W){
							t_x = this.x;
							t_y += FONTSIZE;
						}

					}else{
						t_spritebuffer.visible = false;
					}
				}
			}
		}

		/** Awake
		*/
		public void Awake()
		{
			//rendertexture
			this.rendertexture_w = UnityEngine.Screen.width;
			this.rendertexture_h = UnityEngine.Screen.height;

			//screenparam
			this.screenparam = BlueBack.Gl.ScreenTool.CreateScreenParamWidthStretch(VIRTUAL_SCREEN_W,VIRTUAL_SCREEN_H,this.rendertexture_w,this.rendertexture_h);

			//font
			{
				BlueBack.Font.InitParam t_initparam = BlueBack.Font.InitParam.CreateDefault();
				{
					t_initparam.stringbuffer_capacity = 1024;
					t_initparam.font = new UnityEngine.Font[]{
						UnityEngine.Font.CreateDynamicFontFromOSFont("xxxx",FONTSIZE),
					};
				}

				this.font = new BlueBack.Font.Font(in t_initparam);
			}

			//gl
			{
				BlueBack.Gl.InitParam t_initparam = BlueBack.Gl.InitParam.CreateDefault();
				{
					t_initparam.spritelist_max = 2;
					t_initparam.texture_max = 1;
					t_initparam.material_max = 1;
					t_initparam.sprite_max = 100;
				}
				this.gl = new BlueBack.Gl.Gl(in t_initparam);

				//SetScreenParam
				#if(DEF_BLUEBACK_GL_DEBUGVIEW)
				BlueBack.Gl.Sprite_DebugView_MonoBehaviour.SetScreenParam(in this.screenparam);
				#endif

				//texturelist
				this.gl.texturelist.list[0] = this.font.list[0].font.material.mainTexture;
					
				//materialexecutelist
				this.gl.materialexecutelist.list[0] = new BlueBack.Gl.MaterialExecute_SImple(this.gl,UnityEngine.Resources.Load<UnityEngine.Material>("Font_Simple"));
			}

			//textsprite
			this.textsprite1 = new TextSprite("あいうえおかきくけこさいすせそたちつてとなにぬねのはひふへほまみむめもやゆよわをんabcdefghkjklmnopqrstuvwxyz0123456789",100,100,this);

			//textsprite
			this.textsprite2 = new TextSprite("<>@*",500,500,this);
		}

		/** Start
		*/
		private void Start()
		{
			this.font.list[0].ClearTextureHashSet();
			this.font.list[0].Apply();
		}

		/** Update
		*/
		private void Update()
		{
		}

		/** OnDestroy
		*/
		private void OnDestroy()
		{
			if(this.textsprite1 != null){
				this.textsprite1.Dispose();
				this.textsprite1 = null;
			}

			if(this.textsprite2 != null){
				this.textsprite2.Dispose();
				this.textsprite2 = null;
			}

			if(this.font != null){
				this.font.Dispose();
				this.font = null;
			}

			if(this.gl != null){
				this.gl.Dispose();
				this.gl = null;
			}
		}
	}
}

