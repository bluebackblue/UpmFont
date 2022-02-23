

/** BlueBack.Font.Samples.Simple
*/
namespace BlueBack.Font.Samples.Simple
{
	/** Main_Monobehaviour
	*/
	public sealed class Main_Monobehaviour : UnityEngine.MonoBehaviour , BlueBack.Font.CallBackBeforeBuildWithDirty_Base , BlueBack.Font.CallBackAfterBuild_Base
	{
		/** font
		*/
		public BlueBack.Font.Font font;

		/** gl
		*/
		public BlueBack.Gl.Gl gl;

		/** FONTSIZE
		*/
		public const int FONTSIZE = 64;

		/** FONTINDEX
		*/
		public const int FONTINDEX = 0;

		/** VIRTUAL_SCREEN
		*/
		public const int VIRTUAL_SCREEN_W = 1280;
		public const int VIRTUAL_SCREEN_H = 720;

		/** spriteindex
		*/
		public BlueBack.Gl.SpriteIndex spriteindex;
		public bool spriteindex_create;
		public int spriteindex_x;
		public int spriteindex_y;

		/** charkey
		*/
		public BlueBack.Font.CharKey[] charkey;

		/** Awake
		*/
		public void Awake()
		{
			//font
			{
				BlueBack.Font.InitParam t_initparam = BlueBack.Font.InitParam.CreateDefault();
				{
					t_initparam.stringbuffer_capacity = 1024;
					t_initparam.font = new UnityEngine.Font[]{
						UnityEngine.Font.CreateDynamicFontFromOSFont(UnityEngine.Font.GetOSInstalledFontNames()[0],FONTSIZE),
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
					t_initparam.camera_orthographic_size = 5.0f;
					t_initparam.screenparam = BlueBack.Gl.ScreenTool.CreateScreenParamWidthStretch(VIRTUAL_SCREEN_W,VIRTUAL_SCREEN_H,UnityEngine.Screen.width,UnityEngine.Screen.height);
				}
				this.gl = new BlueBack.Gl.Gl(in t_initparam);

				//SetScreenParam
				#if(DEF_BLUEBACK_GL_DEBUGVIEW)
				BlueBack.Gl.Sprite_DebugView_MonoBehaviour.SetScreenParam(in this.screenparam);
				#endif

				//texturelist
				this.gl.texturelist.list[0] = this.font.GetFont(FONTINDEX).material.mainTexture;
					
				//materialexecutelist
				this.gl.materialexecutelist.list[0] = new BlueBack.Gl.MaterialExecute_SImple(this.gl,UnityEngine.Resources.Load<UnityEngine.Material>("Simple/Font"));
			}

			//uitext
			{
				UnityEngine.UI.Text t_uitext = UnityEngine.GameObject.Find("Text").GetComponent<UnityEngine.UI.Text>();
				t_uitext.font = this.font.GetFont(FONTINDEX);
			}

			//スプライト作成。
			this.spriteindex_x = VIRTUAL_SCREEN_W / 2;
			this.spriteindex_y = VIRTUAL_SCREEN_H / 2;
			this.spriteindex = this.gl.spritelist[0].CreateSprite(false,0,0);
			this.spriteindex_create = false;

			//コールバック登録。
			this.font.SetCallBackBeforeBuildWithDirty(this);
			this.font.SetCallBackAfterBuild(this);

			//文字列。
			this.charkey = new BlueBack.Font.CharKey[]{
				new CharKey('あ',FONTSIZE,UnityEngine.FontStyle.Normal)
			};

			//ビルド。
			this.font.StartBuild();
			this.font.SetDirty(FONTINDEX);
			{
				//文字列追加。
			}
			this.font.EndBuild();
		}

		/** [BlueBack.Font.CallBackBeforeBuildWithDirty_Base]ビルド直前。

			フラグの立っていないフォントはビルドされない。
			すべてのCallBackBeforeBuildが呼び出された後に呼び出される。

		*/
		public void CallBackBeforeBuildWithDirty(bool[] a_dirtyflag)
		{
			if(a_dirtyflag[FONTINDEX] == true){
				this.font.AddString(FONTINDEX,this.charkey);
			}
		}

		/** [BlueBack.Font.CallBackAfterBuild_Base]ビルド直後。

			a_rebultflag : ビルド完了フラグ。

		*/
		public void CallBackAfterBuild(bool[] a_buildflag)
		{
			ref BlueBack.Gl.SpriteBuffer t_spritebuffer = ref this.spriteindex.GetSpriteBuffer();

			if((this.spriteindex_create==false)||(a_buildflag[FONTINDEX]==true)){
				UnityEngine.CharacterInfo t_characterinfo;
				if(this.font.GetCharacterInfo(FONTINDEX,this.charkey[0],out t_characterinfo) == true){
					if(this.spriteindex_create == false){
						t_spritebuffer.visible = true;
						t_spritebuffer.material_index = 0;
						t_spritebuffer.texture_index = 0;
						t_spritebuffer.color = new UnityEngine.Color(1.0f,1.0f,1.0f,1.0f);
						this.spriteindex_create = true;
					}

					//texcord
					t_spritebuffer.texcord = Unity.Mathematics.math.float2x4(
						Unity.Mathematics.math.float2(t_characterinfo.uvTopLeft.x,t_characterinfo.uvTopLeft.y),
						Unity.Mathematics.math.float2(t_characterinfo.uvTopRight.x,t_characterinfo.uvTopRight.y),
						Unity.Mathematics.math.float2(t_characterinfo.uvBottomRight.x,t_characterinfo.uvBottomRight.y),
						Unity.Mathematics.math.float2(t_characterinfo.uvBottomLeft.x,t_characterinfo.uvBottomLeft.y)
					);

					//SetVertex
					BlueBack.Gl.SpriteTool.SetVertex(
						ref t_spritebuffer,
						Unity.Mathematics.math.float2x2(
							Unity.Mathematics.math.float2(t_characterinfo.minX,- t_characterinfo.maxY),
							Unity.Mathematics.math.float2(t_characterinfo.maxX,- t_characterinfo.minY)
						),
						Unity.Mathematics.math.float2(this.spriteindex_x,this.spriteindex_y),
						in this.spriteindex.spritelist.gl.screenparam
					);
				}
			}
		}

		/** OnDestroy
		*/
		private void OnDestroy()
		{
			if(this.font != null){
				this.font.UnSetCallBackBeforeBuildWithDirty(this);
				this.font.UnSetCallBackAfterBuild(this);
			}

			if(this.spriteindex != null){
				this.spriteindex.spritelist.DeleteSprite(this.spriteindex);
				this.spriteindex = null;
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

