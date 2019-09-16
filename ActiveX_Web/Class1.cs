using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Drawing;

namespace ActiveX_Web
{
    public class Class1


    {
        public enum LScanVisMode
        {
            LSCAN_VIS_PREVIEW_ONLY,
            LSCAN_VIS_RESULT,
            LSCAN_VIS_ALWAYS
        }
        public enum ImageFormat
        {
            IMG_FORMAT_GRAY,
            IMG_FORMAT_RGB24,
            IMG_FORMAT_RGB32,
            IMG_FORMAT_UNKNOW,
        }
        public enum enumLScanImageType
        {

            TYPE_NONE,
            ROLL_SINGLE_FINGER,
            FLAT_SINGLE_FINGER,
            FLAT_RIGHT_FINGERS,
            FLAT_LEFT_FINGERS,
            FLAT_TWO_THUMBS,
            PALM_RIGHT_FULL,
            PALM_RIGHT_WRITERS,
            PALM_RIGHT_LOWER,
            PALM_RIGHT_UPPER,
            PALM_LEFT_FULL,
            PALM_LEFT_WRITERS,
            PALM_LEFT_LOWER,
            PALM_LEFT_UPPER,
            FLAT_TWO_FINGERS,
            ROLL_SINGLE_FINGER_VERTICAL

        }
        public class tagImageData
        {

            public int Buffer { get; set; }

            public int Width { get; set; }

            public int Height { get; set; }

            public Double ResolutionX { get; set; }

            public Double ResolutionY { get; set; }

            public Double FrameTime { get; set; }

            public int Pitch { get; set; }

            public Byte BitsPerPixel { get; set; }

            public ImageFormat Format { get; set; }


            public bool IsFina { get; set; }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct LScanDeviceInfo
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string serialNumber;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string typeName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string interfaceType;
        }

        int Fl;
        LScanDeviceInfo deviceinfo;

        /* DLL Import with 1st method */
        [DllImport("LScanEssentials.dll")]
        public static extern int LSCAN_InitAPI();
        [DllImport("LScanEssentials.dll")]
        public static extern int LSCAN_ExitAPI();
        [DllImport("LScanEssentials.dll")]
        public static extern int LSCAN_Main_Release(int handler, bool sendToStandby);

        [DllImport("LScanEssentials.dll")]
        public static extern int LSCAN_Main_Initialize(int device, bool reset, ref int handle);
        [DllImport("LScanEssentials.dll")]
        public static extern int LSCAN_Main_GetDeviceCount(ref int count);

        [DllImport("LScanEssentials.dll")]
        public static extern int LSCAN_Main_GetDeviceInfo(int deviceIndex, ref LScanDeviceInfo Device_Info);
        [DllImport("LScanEssentials.dll")]
        public static extern int LSCAN_Visualization_SetWindow(int handle, int hWnd, tagRECT drawRect);
        [DllImport("LScanEssentials.dll")]
        public static extern int SetVisualizationMode(int handle, LScanVisMode mode, uint options);
        [DllImport("LScanEssentials.dll")]
        public static extern int LSCAN_Capture_SetMode(int handle, int ImateTyp, int LScanImageResolution, int LScanImageOrientation, long REGDWORD,
                                                                                                                           ref int resultWidth, ref int resultHeight, ref int baseResolutionX, ref int baseResolutionY);
        [DllImport("LScanEssentials.dll")]
        public static extern int LSCAN_Capture_Start(int handle, int numberOfObjects);

        [DllImport("LScanEssentials.dll")]
        public static extern int LSCAN_Capture_TakeResultImage(int handle);

        public delegate void ResultImageAvailable(int deviceIndex, ref int objThis, int imageStatus, tagImageData image, enumLScanImageType imageType, int detectedObjects);
        [DllImport("LScanEssentials.dll")]
        public static extern int LSCAN_Capture_RegisterCallbackResultImage(int handle, ResultImageAvailable ResultImageAvailable, ref Class1 context);
        [DllImport("LScanEssentials.dll")]
        public static extern int LSCAN_Controls_Beeper(int handle, int pattern, int volume);
        [ProgId("DemoCSharpActiveX.HelloWorld")]

        [ClassInterface(ClassInterfaceType.AutoDual)]

        [Guid("EDBFDE1E-5A09-4982-A638-91788B827711")]

        [ComVisible(true)]

        public class HelloWord

        {
            int Fl;
            LScanDeviceInfo deviceinfo;


            [ComVisible(true)]

            public String SayHello()

            {

                LScanDeviceInfo deviceinfo;

                return "Hello World!";

            }
            public int DeviceCount()
            {



                int DeviceCount = 0;
                LSCAN_InitAPI();
                int handler = 0;
                int F1;


                //LScanDeviceInfo deviceinfo = null;
                // LSCAN_ExitAPI();
                int FunctionReturnResult = LSCAN_Main_GetDeviceCount(ref DeviceCount);
                //LScanDeviceInfo deviceinfo;



                return DeviceCount;



            }

            public string DeviceInfo()
            {
                LSCAN_Main_GetDeviceInfo(Fl, ref deviceinfo);
                string DeviceName = deviceinfo.typeName;
                return (DeviceName);
            }

            public int Intialize()
            {

                LSCAN_InitAPI();
                int handler = 0;
                int FunctionReturnResult = LSCAN_Main_Initialize(0, false, ref handler);
                return FunctionReturnResult;
            }

            public int SetDeviceReady()
            {
                int FunctionReturnResult;

                LSCAN_Main_Release(0, false);

                int handler = 0;
                FunctionReturnResult = LSCAN_Main_Initialize(1, false, ref handler);

                int resultWidth = 0;
                int resultHeight = 0;
                int baseResolutionX = 0;
                int baseResolutionY = 0;


                tagRECT rct = new tagRECT();


                rct.top = 50;
                rct.left = 50;
                rct.right = 50;
                rct.bottom = 50;


                char vname;

                //vname = LSCAN_Visualization_Create()

                FunctionReturnResult = LSCAN_Visualization_SetWindow(0, 50, rct);
                LScanVisMode mode = LScanVisMode.LSCAN_VIS_PREVIEW_ONLY;
                FunctionReturnResult = SetVisualizationMode(0, mode, 1);

                long REG_DWORD = 0;

                FunctionReturnResult = LSCAN_Capture_SetMode(0, 1, 500, 1, REG_DWORD, ref resultWidth, ref resultHeight, ref baseResolutionX, ref baseResolutionY);
                FunctionReturnResult = LSCAN_Capture_Start(0, 1);
                Class1 ob = null;
                ResultImageAvailable obj = OnResultImageAvailable;
                FunctionReturnResult = LSCAN_Capture_RegisterCallbackResultImage(0, obj, ref ob);

                return FunctionReturnResult;



            }
            public int TakeResultImage()
            {

                int FunctionReturnResult = LSCAN_Capture_TakeResultImage(0);

                return FunctionReturnResult;
            }

            public void OnResultImageAvailable(int deviceHandle, ref int context, int imageStatus, tagImageData imageData, enumLScanImageType imageType, int detectedObjects)
            {


                if (imageStatus < 0)
                {
                    LSCAN_Controls_Beeper(0, 1, 15);

                }

            }
            public void OnResultImageReceived(int deviceHandle, ref int context, int imageStatus, tagImageData imageData, enumLScanImageType imageType, int detectedObjects)
            {

                if (imageStatus == 0)
                {
                    try
                    {




                        int num = Convert.ToInt32((imageData.Width * imageData.Height));
                        int stride = Convert.ToInt32((Size.width * 8 + 7) / 8));
                        var array = new byte[] { (Convert.ToByte(imageData.Height * stride)) };



                        // Marshal.Copy(imageData.Buffer, array, 0, num);



                        Bitmap newImage = new Bitmap(GetImage(array, new Size(Convert.ToInt32(imageData.Width), Convert.ToInt32(imageData.Height)), Convert.ToInt32(imageData.ResolutionX), Convert.ToInt32(imageData.ResolutionY)));
                        if (newImage is  null){
                            return;
                        }

                        // wsServer.setImageValue(newImage);

                        if (detectedObjects != 0)
                        {
                            //EventExtension.Raise(Of ImageEventArgs)(Me.ImageCaptured, Me, New ImageEventArgs(image, Me._captureContext.get_Position(), Me._captureContext.get_IsForceCapture()))
                            //'Me.ClearLEDs()
                        }
                        else
                        {
                            string text = "No objects detected in capture area";
                        }

                        return;
                    }
                    catch (Exception e)
                    {

                        return;
                    }
                }




            }


        }
    }
}


