

/** BlueBack.Font.Samples.Simple
*/
#if(!DEF_BLUEBACK_FONT_SAMPLES_DISABLE)
namespace BlueBack.Font.Samples.Simple
{
	/** Main_Monobehaviour
	*/
	public sealed class Main_Monobehaviour : UnityEngine.MonoBehaviour , BlueBack.Font.CallBackAddString_Base , BlueBack.Font.CallBackReCalcUv_Base
	{
		/** font
		*/
		public BlueBack.Font.Font font;

		/** gl
		*/
		public BlueBack.Gl.Gl gl;

		/** MATERIALINDEX
		*/
		public const int MATERIALINDEX = 0;

		/** FONTSIZE
		*/
		public const int FONTSIZE = 64;

		/** FONTINDEX
		*/
		public const int FONTINDEX_1 = 0;
		public const int FONTINDEX_2 = 1;

		/** TEXTUREINDEX
		*/
		public const int TEXTUREINDEX_1 = 0;
		public const int TEXTUREINDEX_2 = 1;

		/** VIRTUAL_SCREEN
		*/
		public const int VIRTUAL_SCREEN_W = 1280;
		public const int VIRTUAL_SCREEN_H = 720;

		/** spriteindex
		*/
		public BlueBack.Gl.SpriteIndex spriteindex_1;
		public BlueBack.Gl.SpriteIndex spriteindex_2;
		public int spriteindex1_x;
		public int spriteindex1_y;
		public int spriteindex2_x;
		public int spriteindex2_y;

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
					string[] t_fontname_list = UnityEngine.Font.GetOSInstalledFontNames();

					#if(DEF_LISTUPOSFONTNAME)
					for(int ii=0;ii<t_fontname_list.Length;ii++){
						UnityEngine.Debug.Log(t_fontname_list[ii]);
					}
					#endif

					t_initparam.stringbuffer_capacity = 1024;
					t_initparam.font = new UnityEngine.Font[]{
						UnityEngine.Font.CreateDynamicFontFromOSFont(t_fontname_list[0],FONTSIZE),
						UnityEngine.Font.CreateDynamicFontFromOSFont("MS Mincho",FONTSIZE),
					};
				}

				this.font = new BlueBack.Font.Font(in t_initparam);
			}

			//gl
			{
				BlueBack.Gl.InitParam t_initparam = BlueBack.Gl.InitParam.CreateDefault();
				{
					t_initparam.spritelist = new Gl.InitParam.SpriteList[]{
						new Gl.InitParam.SpriteList(){
							sprite_max = 100,
						},
						new Gl.InitParam.SpriteList(){
							sprite_max = 100,
						},
						new Gl.InitParam.SpriteList(){
							sprite_max = 100,
						},
						new Gl.InitParam.SpriteList(){
							sprite_max = 100,
						},
					};
					t_initparam.texture_max = 2;
					t_initparam.material_max = 1;
					t_initparam.camera_orthographic_size = 5.0f;
					t_initparam.screenparam = BlueBack.Gl.ScreenTool.CreateScreenParamWidthStretch(VIRTUAL_SCREEN_W,VIRTUAL_SCREEN_H,UnityEngine.Screen.width,UnityEngine.Screen.height);
				}
				this.gl = new BlueBack.Gl.Gl(in t_initparam);

				//SetScreenParam
				#if(DEF_BLUEBACK_GL_DEBUGVIEW)
				BlueBack.Gl.Sprite_DebugView_MonoBehaviour.SetScreenParam(in this.screenparam);
				#endif

				//texturelist
				this.gl.texturelist.list[TEXTUREINDEX_1] = this.font.fontlist.GetFont(FONTINDEX_1).material.mainTexture;
				this.gl.texturelist.list[TEXTUREINDEX_2] = this.font.fontlist.GetFont(FONTINDEX_2).material.mainTexture;
					
				//materialexecutelist
				this.gl.materialexecutelist.list[MATERIALINDEX] = new BlueBack.Gl.MaterialExecute_SImple(this.gl,UnityEngine.Resources.Load<UnityEngine.Material>("BlueBack.Font.Samples.Simple/Font"));
			}

			//uitext
			{
				UnityEngine.UI.Text t_uitext = UnityEngine.GameObject.Find("Text").GetComponent<UnityEngine.UI.Text>();
				t_uitext.font = this.font.fontlist.GetFont(FONTINDEX_1);
			}

			//スプライト作成。
			this.spriteindex1_x = VIRTUAL_SCREEN_W / 2;
			this.spriteindex1_y = VIRTUAL_SCREEN_H / 2;
			this.spriteindex2_x = VIRTUAL_SCREEN_W / 2 + 100;
			this.spriteindex2_y = VIRTUAL_SCREEN_H / 2;
			this.spriteindex_1 = this.gl.spritelist[0].CreateSprite(false,MATERIALINDEX,TEXTUREINDEX_1);
			this.spriteindex_2 = this.gl.spritelist[0].CreateSprite(false,MATERIALINDEX,TEXTUREINDEX_2);

			//文字。
			this.charkey = new BlueBack.Font.CharKey[]{
				new CharKey('あ',FONTSIZE,UnityEngine.FontStyle.Normal)
			};

			//ビルド。開始。
			this.font.StartBuild();

			this.font.fontlist.AddString(FONTINDEX_1,this.charkey);
			this.font.fontlist.AddString(FONTINDEX_2,this.charkey);

			//ビルド。終了。
			this.font.EndBuild();

			{
				ref BlueBack.Gl.SpriteBuffer t_spritebuffer = ref this.spriteindex_1.GetSpriteBuffer();
				t_spritebuffer.visible = true;
				t_spritebuffer.color = new UnityEngine.Color(1.0f,1.0f,1.0f,1.0f);
				Inner_CalcTexcordVertex(FONTINDEX_1,ref t_spritebuffer,this.spriteindex1_x,this.spriteindex1_y,in this.gl.screenparam);
			}

			{
				ref BlueBack.Gl.SpriteBuffer t_spritebuffer = ref this.spriteindex_2.GetSpriteBuffer();
				t_spritebuffer.visible = true;
				t_spritebuffer.color = new UnityEngine.Color(1.0f,1.0f,1.0f,1.0f);
				Inner_CalcTexcordVertex(FONTINDEX_2,ref t_spritebuffer,this.spriteindex2_x,this.spriteindex2_y,in this.gl.screenparam);
			}

			//コールバック登録。
			this.font.callbacklist.SetCallBackAddString(this);
			this.font.callbacklist.SetCallBackReCalcUv(this);
		}

		/** Inner_CalcTexcordVertex
		*/
		private void Inner_CalcTexcordVertex(int a_fontindex,ref BlueBack.Gl.SpriteBuffer a_spritebuffer,int a_x,int a_y,in BlueBack.Gl.ScreenParam a_screenparam)
		{
			UnityEngine.CharacterInfo t_characterinfo;
			if(this.font.fontlist.GetCharacterInfo(a_fontindex,this.charkey[0],out t_characterinfo) == true){
				//texcord
				a_spritebuffer.texcord = Unity.Mathematics.math.float2x4(
					Unity.Mathematics.math.float2(t_characterinfo.uvTopLeft.x,t_characterinfo.uvTopLeft.y),
					Unity.Mathematics.math.float2(t_characterinfo.uvTopRight.x,t_characterinfo.uvTopRight.y),
					Unity.Mathematics.math.float2(t_characterinfo.uvBottomRight.x,t_characterinfo.uvBottomRight.y),
					Unity.Mathematics.math.float2(t_characterinfo.uvBottomLeft.x,t_characterinfo.uvBottomLeft.y)
				);

				//SetVertex
				BlueBack.Gl.SpriteTool.SetVertex(
					ref a_spritebuffer,
					Unity.Mathematics.math.float2x2(
						Unity.Mathematics.math.float2(t_characterinfo.minX,- t_characterinfo.maxY),
						Unity.Mathematics.math.float2(t_characterinfo.maxX,- t_characterinfo.minY)
					),
					Unity.Mathematics.math.float2(a_x,a_y),
					in a_screenparam
				);
			}
		}

		/** [BlueBack.Font.CallBackAddString_Base]文字追加コールバック。

			a_buildrequest == true : ビルドリクエストあり。

		*/
		public void CallBackAddString(bool[] a_buildrequest)
		{
			UnityEngine.Debug.Log("CallBackAddString");

			if(a_buildrequest[FONTINDEX_1] == true){
				UnityEngine.Debug.Log("CallBackAddString : FONTINDEX_1");
				this.font.fontlist.AddString(FONTINDEX_1,this.charkey);
			}

			if(a_buildrequest[FONTINDEX_2] == true){
				UnityEngine.Debug.Log("CallBackAddString : FONTINDEX_2");
				this.font.fontlist.AddString(FONTINDEX_2,this.charkey);
			}
		}

		/** [BlueBack.Font.CallBackReCalcUv_Base]ＵＶ再計算コールバック。

			a_buildrequest == true		: ビルドリクエストあり。
			a_changetexture == true		: フォントテクスチャーが再構築された。

		*/
		public void CallBackReCalcUv(bool[] a_buildrequest,bool[] a_changetexture)
		{
			UnityEngine.Debug.Log("CallBackReCalcUv");

			if(a_changetexture[FONTINDEX_1] == true){
				UnityEngine.Debug.Log("CallBackReCalcUv : FONTINDEX_1");
				Inner_CalcTexcordVertex(FONTINDEX_1,ref this.spriteindex_1.GetSpriteBuffer(),this.spriteindex1_x,this.spriteindex1_y,in this.gl.screenparam);
			}

			if(a_changetexture[FONTINDEX_2] == true){
				UnityEngine.Debug.Log("CallBackReCalcUv : FONTINDEX_2");
				Inner_CalcTexcordVertex(FONTINDEX_2,ref this.spriteindex_2.GetSpriteBuffer(),this.spriteindex2_x,this.spriteindex2_y,in this.gl.screenparam);
			}
		}

		/** OnDestroy
		*/
		private void OnDestroy()
		{
			if(this.font != null){
				this.font.callbacklist.UnSetCallBackAddString(this);
				this.font.callbacklist.UnSetCallBackReCalcUv(this);
			}

			if(this.spriteindex_1 != null){
				this.spriteindex_1.spritelist.DeleteSprite(this.spriteindex_1);
				this.spriteindex_1 = null;
			}

			if(this.spriteindex_2 != null){
				this.spriteindex_2.spritelist.DeleteSprite(this.spriteindex_2);
				this.spriteindex_2 = null;
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
#endif

