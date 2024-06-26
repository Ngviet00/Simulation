﻿using ActUtlType64Lib;
using log4net;
using System.IO.Ports;
using System.Timers;

namespace Stiffiner_Inspection
{
    public class ControlPLC
    {
        private ActUtlType64 _plc = new ActUtlType64();
        private const int _plcStation = 1;
        private bool isExist = false;
        private const int timeSleep = 100;
        private readonly ILog _logger = LogManager.GetLogger(typeof(ControlPLC));

        public static System.Timers.Timer? timer = null;

        // Register read
        private const string REG_PLC_Read_STATUS = "D20";
        private const string REG_PLC_RefeshData = "M2010";
        private const string REG_PLC_EndInspection = "M2008";
        private const string REG_PLC_VisionDoneInspection = "M240";

        // Register Write
        private const string REG_PLC_Write = "D";
        private const int REG_PLC_Start = 900;
        private const string REG_Vision_Busy = "M420";
        private const string REG_PLC_NOT_ENOUGHT_TRAY = "M421";

        private bool isStart = false;
        private bool isEMG = false;
        private bool isStop = false;
        private bool isAlarm = false;

        public bool IsStart { get => isStart; set => isStart = value; }
        public bool IsStop { get => isStop; set => isStop = value; }
        public bool IsAlarm { get => isAlarm; set => isAlarm = value; }
        public bool IsEMG { get => isEMG; set => isEMG = value; }

        bool isStartHistory = false;
        bool isEndHistory = false;

        public ControlPLC()
        {
            _plc.ActLogicalStationNumber = _plcStation;
        }

        public void Connect()
        {
            if (_plc.Open() == 0)
            {
                //Thread thread = new Thread(ReadDataFromRegister);
                //thread.IsBackground = true;
                //thread.Name = "REG_PLC_STATUS";
                //thread.Start();

                //Thread thread1 = new Thread(ReadEventStartFromPLC);
                //thread1.IsBackground = true;
                //thread1.Name = "ReadEventStartFromPLC";
                //thread1.Start();
            }
            else
            {
                //_logger.Error("Can not connect to PLC");
            }
        }

        //private void ReadDataFromRegister()
        //{
        //    while (!isExist)
        //    {
        //        int valueReaded = 0;
        //        _plc.ReadDeviceBlock(REG_PLC_Read_STATUS, 1, out valueReaded);
        //        SetStatusOfMachine(valueReaded);
        //        Global.valuePLC = valueReaded;
        //        Thread.Sleep(timeSleep);
        //    }
        //}

        //private void ReadEventStartFromPLC()
        //{
        //    while (!isExist)
        //    {
        //        int valueReaded = 0;
        //        _plc.GetDevice(REG_PLC_RefeshData, out valueReaded);

        //        if (valueReaded == 0) isStartHistory = false;
        //        //kiem tra neu start nhan thi gui cho clent tin hieu star de clear tray
        //        if (!isStartHistory && valueReaded == 1)
        //        {
        //            Global.resetPLC1 = 1;
        //            Global.resetPLC2 = 1;
        //            Global.resetPLC3 = 1;
        //            Global.resetPLC4 = 1;
        //            Global.resetClient = 1;
        //            Global.currentTray++;

        //            Global.CurrentTrayDataV2.Clear();

        //            TurnOnLightControl();
        //            isStartHistory = true;
        //        }
        //        else
        //        {
        //            Global.resetClient = 0;
        //        }

        //        // Check End Insection signal
        //        int valueReadedEndInspection = 0;
        //        _plc.GetDevice(REG_PLC_EndInspection, out valueReadedEndInspection);
        //        if (valueReadedEndInspection == 0) isEndHistory = false;

        //        if (!isEndHistory && valueReadedEndInspection == 1)
        //        {
        //            isEndHistory = true;
        //            TurnOffLightControl();

        //            if (timer != null)
        //            {
        //                timer.Stop();
        //                timer.Dispose();
        //                timer = null;
        //            }

        //            //check if after 5s, tray not enough will send signal to PLC vision not enough tray
        //            timer = new System.Timers.Timer(5000);
        //            timer.Elapsed += TimerCheckVisionEnoughTray;
        //            timer.Start();
        //        }

        //        if (valueReadedEndInspection != 1)
        //        {
        //            timer?.Stop();
        //            timer?.Dispose();
        //            timer = null;
        //        }

        //        Thread.Sleep(timeSleep);
        //    }
        //}

        //public void TimerCheckVisionEnoughTray(object sender, ElapsedEventArgs e)
        //{
        //    timer?.Stop();
        //    timer?.Dispose();
        //    timer = null;

        //    if (Global.CurrentTrayDataV2.Count < 80 && Global.CurrentTrayDataV2.Count > 0)
        //    {
        //        if (Global.currentTray > 0)
        //        {
        //            Global.currentTray -= 1;
        //        }

        //        VisionNotEnoughTray();
        //    }
        //}

        private void SetStatusOfMachine(int binary)
        {
            isAlarm = isStart = isStop = isEMG = false;

            switch (binary)
            {
                case 0:
                    isEMG = true;
                    break;
                case 1:
                    isStart = true;
                    break;
                case 2:
                    isStop = true;
                    break;
                case 3:
                    isAlarm = true;
                    break;
                default:
                    break;
            }
        }

        public void WriteDataToRegister(int data, int index)
        {
            _plc.WriteDeviceBlock(GetWriteRegisterByIndex(index), 1, data);
        }

        private string GetWriteRegisterByIndex(int index)
        {
            return string.Format("{0}{1}", REG_PLC_Write, REG_PLC_Start + index);
        }

        public void TurnOnLightControl()
        {
            try
            {
                SerialPort lightControl1 = new SerialPort("COM4", 115200);
                SerialPort lightControl2 = new SerialPort("COM5", 115200);

                lightControl1.Open();
                lightControl2.Open();

                lightControl1.WriteLine("@SI00/255/255/255/255");
                lightControl2.WriteLine("@SI00/255/255/255/255");

                lightControl1.Close();
                lightControl2.Close();
            }
            catch (Exception ex)
            {
                _logger.Error("Cannot turn on light control: " + ex.Message);
            }
        }

        public void TurnOffLightControl()
        {
            try
            {
                SerialPort lightControl1 = new SerialPort("COM4", 115200);
                SerialPort lightControl2 = new SerialPort("COM5", 115200);

                lightControl1.Open();
                lightControl2.Open();

                lightControl1.WriteLine("@SI00/0/0/0/0");
                lightControl2.WriteLine("@SI00/0/0/0/0");

                lightControl1.Close();
                lightControl2.Close();
            }
            catch (Exception ex)
            {
                _logger.Error("Cannot turn off the light: " + ex.Message);
            }

        }

        //busy = 1, ready 0
        public void VisionBusy(bool status)
        {
            _plc.SetDevice(REG_Vision_Busy, status ? 1 : 0);
        }

        public void VisionDoneIns()
        {
            _plc.SetDevice(REG_PLC_VisionDoneInspection, 1);
        }

        public void VisionNotEnoughTray()
        {
            _plc.SetDevice(REG_PLC_NOT_ENOUGHT_TRAY, 1);
        }
    }
}
