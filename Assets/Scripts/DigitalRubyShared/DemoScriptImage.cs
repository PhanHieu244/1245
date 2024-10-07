using System;
using System.Collections.Generic;

namespace DigitalRubyShared
{
	public class DemoScriptImage : FingersImageAutomationScript
	{
		private const int imageWidth = 16;

		private const float xPadding = 0.03f;

		private static readonly Dictionary<ImageGestureImage, string> recognizableImages;

		protected override void Start()
		{
			this.RecognizableImages = DemoScriptImage.recognizableImages;
			base.Start();
			this.ImageGesture.MaximumPathCount = 2;
		}

		public void CopyScriptToClipboard()
		{
		}

		static DemoScriptImage()
		{
			// Note: this type is marked as 'beforefieldinit'.
			Dictionary<ImageGestureImage, string> dictionary = new Dictionary<ImageGestureImage, string>();
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				60uL,
				62uL,
				63uL,
				63uL,
				63uL,
				59uL,
				59uL,
				120uL,
				120uL,
				112uL,
				112uL,
				112uL,
				112uL,
				112uL,
				240uL,
				240uL
			}, 16), "Check Mark");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				60uL,
				126uL,
				127uL,
				255uL,
				503uL,
				483uL,
				963uL,
				1984uL,
				3968uL,
				3840uL,
				7680uL,
				15872uL,
				15360uL,
				30720uL,
				63488uL,
				61440uL
			}, 16), "Check Mark");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				126uL,
				255uL,
				511uL,
				999uL,
				1991uL,
				3971uL,
				7939uL,
				15872uL,
				31744uL,
				63488uL,
				61440uL,
				57344uL,
				49152uL,
				0uL,
				0uL,
				0uL
			}, 16), "Check Mark");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				511uL,
				4095uL,
				16359uL,
				65415uL,
				64515uL,
				61443uL,
				49152uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL
			}, 16), "Check Mark");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				2044uL,
				4094uL,
				3870uL,
				7695uL,
				7695uL,
				15367uL,
				15367uL,
				30723uL,
				30723uL,
				61440uL,
				61440uL,
				57344uL,
				57344uL,
				49152uL,
				0uL,
				0uL
			}, 16), "Check Mark");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				126uL,
				126uL,
				127uL,
				127uL,
				247uL,
				247uL,
				227uL,
				227uL,
				480uL,
				480uL,
				448uL,
				960uL,
				960uL,
				896uL,
				896uL,
				896uL
			}, 16), "Check Mark");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				14uL,
				31uL,
				63uL,
				63uL,
				127uL,
				123uL,
				243uL,
				499uL,
				480uL,
				960uL,
				1984uL,
				3968uL,
				3840uL,
				7680uL,
				15872uL,
				15360uL
			}, 16), "Check Mark");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				504uL,
				508uL,
				1020uL,
				990uL,
				1951uL,
				1935uL,
				3847uL,
				3847uL,
				7683uL,
				15875uL,
				15360uL,
				30720uL,
				30720uL,
				61440uL,
				61440uL,
				57344uL
			}, 16), "Check Mark");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				510uL,
				1023uL,
				1999uL,
				3975uL,
				7939uL,
				7680uL,
				7168uL,
				7168uL,
				15360uL,
				15360uL,
				14336uL,
				30720uL,
				30720uL,
				28672uL,
				61440uL,
				61440uL
			}, 16), "Check Mark");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				255uL,
				2047uL,
				16379uL,
				65475uL,
				65027uL,
				61440uL,
				49152uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL
			}, 16), "Check Mark");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				127uL,
				255uL,
				1019uL,
				4067uL,
				8131uL,
				32515uL,
				64515uL,
				63491uL,
				57344uL,
				49152uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL
			}, 16), "Check Mark");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				62uL,
				127uL,
				255uL,
				503uL,
				995uL,
				1987uL,
				1920uL,
				3840uL,
				7936uL,
				15872uL,
				31744uL,
				63488uL,
				61440uL,
				57344uL,
				49152uL,
				0uL
			}, 16), "Check Mark");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				15uL,
				31uL,
				63uL,
				127uL,
				251uL,
				496uL,
				992uL,
				1984uL,
				3968uL,
				7936uL,
				15872uL,
				31744uL,
				63488uL,
				61440uL,
				57344uL,
				49152uL
			}, 16), "Check Mark");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				127uL,
				255uL,
				499uL,
				2019uL,
				4035uL,
				8064uL,
				15872uL,
				64512uL,
				63488uL,
				61440uL,
				49152uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL
			}, 16), "Check Mark");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				7uL,
				15uL,
				31uL,
				62uL,
				124uL,
				248uL,
				496uL,
				2047uL,
				2047uL,
				2047uL,
				1823uL,
				62uL,
				124uL,
				248uL,
				496uL,
				480uL
			}, 16), "Lightning Bolt");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				112uL,
				112uL,
				112uL,
				112uL,
				112uL,
				112uL,
				127uL,
				127uL,
				127uL,
				127uL,
				123uL,
				115uL,
				3uL,
				3uL,
				3uL,
				3uL
			}, 16), "Lightning Bolt");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				3uL,
				7uL,
				7uL,
				15uL,
				15uL,
				31uL,
				31uL,
				63uL,
				63uL,
				63uL,
				15uL,
				15uL,
				30uL,
				30uL,
				60uL,
				60uL
			}, 16), "Lightning Bolt");
			Dictionary<ImageGestureImage, string> arg_290_0 = dictionary;
			ulong[] expr_272 = new ulong[16];
			expr_272[0] = 65535uL;
			expr_272[1] = 65535uL;
			arg_290_0.Add(new ImageGestureImage(expr_272, 16), "Horizontal Line");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				3uL,
				3uL,
				3uL,
				3uL,
				3uL,
				3uL,
				3uL,
				3uL,
				3uL,
				3uL,
				3uL,
				3uL,
				3uL,
				3uL,
				3uL,
				3uL
			}, 16), "Vertical Line");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				3uL,
				7uL,
				15uL,
				31uL,
				62uL,
				124uL,
				248uL,
				496uL,
				992uL,
				1984uL,
				3968uL,
				7936uL,
				15872uL,
				31744uL,
				63488uL,
				61440uL
			}, 16), "Diagonal Line /");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				3uL,
				3uL,
				3uL,
				7uL,
				7uL,
				7uL,
				7uL,
				15uL,
				15uL,
				14uL,
				14uL,
				30uL,
				30uL,
				28uL,
				60uL,
				60uL
			}, 16), "Diagonal Line /");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				3uL,
				7uL,
				7uL,
				15uL,
				15uL,
				30uL,
				30uL,
				60uL,
				60uL,
				120uL,
				120uL,
				240uL,
				240uL,
				480uL,
				992uL,
				960uL
			}, 16), "Diagonal Line /");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				31uL,
				63uL,
				254uL,
				1016uL,
				4080uL,
				8128uL,
				32512uL,
				64512uL,
				63488uL,
				57344uL,
				49152uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL
			}, 16), "Diagonal Line /");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				127uL,
				2047uL,
				16380uL,
				65504uL,
				65024uL,
				61440uL,
				49152uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL
			}, 16), "Diagonal Line /");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				2047uL,
				65535uL,
				65520uL,
				65024uL,
				49152uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL
			}, 16), "Diagonal Line /");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				15uL,
				31uL,
				62uL,
				124uL,
				248uL,
				1008uL,
				2016uL,
				4032uL,
				7936uL,
				15872uL,
				31744uL,
				63488uL,
				61440uL,
				57344uL,
				49152uL,
				0uL
			}, 16), "Diagonal Line /");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				3uL,
				7uL,
				15uL,
				15uL,
				30uL,
				62uL,
				124uL,
				120uL,
				240uL,
				496uL,
				992uL,
				960uL,
				1920uL,
				3968uL,
				7936uL,
				7680uL
			}, 16), "Diagonal Line /");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				15uL,
				31uL,
				62uL,
				124uL,
				248uL,
				496uL,
				992uL,
				1984uL,
				3968uL,
				7936uL,
				15872uL,
				31744uL,
				63488uL,
				61440uL,
				57344uL,
				49152uL
			}, 16), "Diagonal Line /");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				63uL,
				255uL,
				1020uL,
				4080uL,
				16320uL,
				65280uL,
				64512uL,
				61440uL,
				49152uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL
			}, 16), "Diagonal Line /");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				7680uL,
				7936uL,
				3968uL,
				1920uL,
				960uL,
				992uL,
				496uL,
				240uL,
				120uL,
				124uL,
				62uL,
				30uL,
				15uL,
				15uL,
				7uL,
				3uL
			}, 16), "Diagonal Line \\");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				960uL,
				992uL,
				480uL,
				240uL,
				240uL,
				120uL,
				120uL,
				60uL,
				62uL,
				30uL,
				15uL,
				15uL,
				7uL,
				7uL,
				3uL,
				3uL
			}, 16), "Diagonal Line \\");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				57344uL,
				61440uL,
				63488uL,
				31744uL,
				15872uL,
				7936uL,
				3968uL,
				1984uL,
				992uL,
				496uL,
				248uL,
				124uL,
				62uL,
				31uL,
				15uL,
				7uL
			}, 16), "Diagonal Line \\");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				65024uL,
				65408uL,
				8128uL,
				2032uL,
				508uL,
				255uL,
				63uL,
				15uL,
				3uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL
			}, 16), "Diagonal Line \\");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				120uL,
				120uL,
				60uL,
				60uL,
				28uL,
				30uL,
				30uL,
				14uL,
				14uL,
				15uL,
				15uL,
				7uL,
				7uL,
				7uL,
				3uL,
				3uL
			}, 16), "Diagonal Line \\");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				61440uL,
				64512uL,
				32256uL,
				16256uL,
				4032uL,
				2032uL,
				508uL,
				254uL,
				63uL,
				15uL,
				7uL,
				3uL,
				0uL,
				0uL,
				0uL,
				0uL
			}, 16), "Diagonal Line \\");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				16380uL,
				32766uL,
				30751uL,
				61455uL,
				61447uL,
				57347uL,
				49155uL,
				49155uL,
				49155uL,
				57347uL,
				61447uL,
				61455uL,
				30751uL,
				32574uL,
				16382uL,
				16380uL
			}, 16, 0.05f), "Circle");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				4094uL,
				4095uL,
				7695uL,
				7687uL,
				15363uL,
				15363uL,
				14339uL,
				14339uL,
				14339uL,
				14339uL,
				14339uL,
				15367uL,
				15887uL,
				16287uL,
				4094uL,
				2046uL
			}, 16, 0.05f), "Circle");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				65535uL,
				65535uL,
				57359uL,
				57347uL,
				49155uL,
				49155uL,
				57351uL,
				57359uL,
				63551uL,
				65535uL,
				32764uL,
				8176uL,
				0uL,
				0uL,
				0uL,
				0uL
			}, 16, 0.05f), "Circle");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				8191uL,
				16383uL,
				31751uL,
				63491uL,
				61443uL,
				57347uL,
				57351uL,
				57351uL,
				57359uL,
				49167uL,
				57374uL,
				65534uL,
				65532uL,
				32760uL,
				7280uL,
				0uL
			}, 16, 0.05f), "Circle");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				8190uL,
				8191uL,
				15375uL,
				15367uL,
				30727uL,
				30723uL,
				30723uL,
				28675uL,
				28675uL,
				30723uL,
				30723uL,
				14339uL,
				14339uL,
				14339uL,
				14339uL,
				14339uL
			}, 16, 0.05f), "U");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				16382uL,
				32767uL,
				63519uL,
				61447uL,
				57351uL,
				57351uL,
				57347uL,
				57347uL,
				49155uL,
				49155uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL,
				0uL
			}, 16, 0.05f), "U");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				992uL,
				2032uL,
				2032uL,
				3960uL,
				8056uL,
				15932uL,
				31804uL,
				30750uL,
				28702uL,
				61455uL,
				61455uL,
				61447uL,
				57351uL,
				49155uL,
				49155uL,
				0uL
			}, 16), "V");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				480uL,
				992uL,
				1008uL,
				2032uL,
				2040uL,
				3960uL,
				3900uL,
				7740uL,
				7710uL,
				15390uL,
				15375uL,
				30735uL,
				63495uL,
				61447uL,
				57347uL,
				57347uL
			}, 16), "V");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				240uL,
				240uL,
				504uL,
				504uL,
				1020uL,
				1020uL,
				924uL,
				1950uL,
				1950uL,
				3855uL,
				3855uL,
				7943uL,
				7687uL,
				15363uL,
				15363uL,
				14339uL
			}, 16), "V");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				4064uL,
				8176uL,
				7928uL,
				7292uL,
				15422uL,
				15390uL,
				30735uL,
				30735uL,
				61447uL,
				61443uL,
				57347uL,
				57344uL,
				49152uL,
				0uL,
				0uL,
				0uL
			}, 16), "V");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				3968uL,
				4032uL,
				8128uL,
				8160uL,
				15856uL,
				15600uL,
				30840uL,
				30840uL,
				61500uL,
				61500uL,
				57374uL,
				57375uL,
				49167uL,
				7uL,
				7uL,
				3uL
			}, 16), "V");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				3847uL,
				3975uL,
				1999uL,
				975uL,
				510uL,
				510uL,
				252uL,
				248uL,
				248uL,
				508uL,
				1020uL,
				2014uL,
				1951uL,
				3855uL,
				7943uL,
				7683uL
			}, 16, 0.03f), "X");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				63491uL,
				64519uL,
				15879uL,
				7695uL,
				3871uL,
				4030uL,
				2044uL,
				1016uL,
				1008uL,
				2032uL,
				2040uL,
				3964uL,
				7998uL,
				15903uL,
				31759uL,
				30727uL
			}, 16, 0.03f), "X");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				64519uL,
				65311uL,
				16383uL,
				4094uL,
				2040uL,
				8184uL,
				16382uL,
				65151uL,
				63519uL,
				61447uL,
				49155uL,
				3uL,
				0uL,
				0uL,
				0uL,
				0uL
			}, 16, 0.03f), "X");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				31772uL,
				32316uL,
				8060uL,
				4088uL,
				2032uL,
				1008uL,
				496uL,
				1016uL,
				2044uL,
				4030uL,
				7967uL,
				16143uL,
				31751uL,
				63491uL,
				61440uL,
				57344uL
			}, 16, 0.03f), "X");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				61471uL,
				63519uL,
				31804uL,
				15996uL,
				8184uL,
				4080uL,
				2016uL,
				2016uL,
				4080uL,
				8184uL,
				15996uL,
				31806uL,
				63519uL,
				61455uL,
				57351uL,
				57347uL
			}, 16, 0.03f), "X");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				61440uL,
				63488uL,
				31751uL,
				15887uL,
				7743uL,
				4094uL,
				4092uL,
				2032uL,
				4064uL,
				8160uL,
				32752uL,
				64760uL,
				63612uL,
				57406uL,
				49182uL,
				14uL
			}, 16, 0.03f), "X");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				57359uL,
				61471uL,
				63614uL,
				31996uL,
				16376uL,
				8160uL,
				4032uL,
				8064uL,
				16320uL,
				65504uL,
				63984uL,
				61688uL,
				57468uL,
				60uL,
				28uL,
				28uL
			}, 16, 0.03f), "X");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				57344uL,
				61440uL,
				63488uL,
				31747uL,
				15879uL,
				7943uL,
				3983uL,
				2015uL,
				1022uL,
				508uL,
				248uL,
				252uL,
				510uL,
				1023uL,
				1999uL,
				1927uL
			}, 16, 0.03f), "X");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				7683uL,
				7951uL,
				3999uL,
				2047uL,
				1020uL,
				504uL,
				2040uL,
				4092uL,
				16318uL,
				32287uL,
				64527uL,
				61447uL,
				57347uL,
				49155uL,
				49152uL,
				0uL
			}, 16, 0.03f), "X");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				63488uL,
				64515uL,
				15875uL,
				7943uL,
				4031uL,
				2047uL,
				1022uL,
				4080uL,
				16376uL,
				65404uL,
				64574uL,
				61471uL,
				15uL,
				7uL,
				3uL,
				0uL
			}, 16, 0.03f), "X");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				57375uL,
				61567uL,
				63740uL,
				32760uL,
				16352uL,
				8128uL,
				16128uL,
				32640uL,
				65408uL,
				62336uL,
				58240uL,
				49152uL,
				0uL,
				0uL,
				0uL,
				0uL
			}, 16, 0.03f), "X");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				57344uL,
				61440uL,
				63488uL,
				31804uL,
				15996uL,
				8188uL,
				4080uL,
				2016uL,
				2016uL,
				4080uL,
				8184uL,
				7804uL,
				7230uL,
				31uL,
				15uL,
				7uL
			}, 16, 0.03f), "X");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				64527uL,
				65055uL,
				8063uL,
				4092uL,
				2040uL,
				2016uL,
				8160uL,
				16352uL,
				32496uL,
				30968uL,
				28796uL,
				60uL,
				30uL,
				31uL,
				15uL,
				7uL
			}, 16, 0.03f), "X");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				63491uL,
				65027uL,
				16135uL,
				8071uL,
				2031uL,
				1007uL,
				510uL,
				254uL,
				124uL,
				124uL,
				254uL,
				511uL,
				1007uL,
				1991uL,
				1923uL,
				1795uL
			}, 16, 0.03f), "X");
			dictionary.Add(new ImageGestureImage(new ulong[]
			{
				7uL,
				49167uL,
				57375uL,
				61502uL,
				63612uL,
				31992uL,
				16368uL,
				8160uL,
				4032uL,
				4032uL,
				8160uL,
				16368uL,
				31992uL,
				63612uL,
				61500uL,
				57372uL
			}, 16, 0.03f), "X");
			DemoScriptImage.recognizableImages = dictionary;
		}
	}
}
