using System;
using System.IO.Ports;

namespace ConsoleApp2
{
    class Program
    {
        static SerialPort _serialPort;
        static bool _continue;

        static void Main(string[] args)
        {
            //Console.WriteLine(args[0]);

            if (args.Length == 0)
            {
                Console.WriteLine("Favor insira um parametro ou -h para ver opções.");
            }
            else
            {
                _continue = true;
                string data_rec;

                Serial_configAndOpen(args[0]);

                foreach (string aux in args)
                {
                    switch (aux)
                    {
                        case "-0":
                            Serial_write("0");
                            data_rec = Serial_read();
                            Console.WriteLine(data_rec);
                            break;
                        case "-1":
                            Serial_write("1");
                            data_rec = Serial_read();
                            Console.WriteLine(data_rec);
                            break;
                        case "-2":
                            Serial_write("2");
                            data_rec = Serial_read();
                            Console.WriteLine(data_rec);
                            break;
                        case "-3":
                            Serial_write("3");
                            data_rec = Serial_read();
                            Console.WriteLine(data_rec);
                            break;
                        case "-4":
                            Serial_write("4");
                            data_rec = Serial_read();
                            Console.WriteLine(data_rec);
                            break;
                        case "-5":
                            Serial_write("5");
                            data_rec = Serial_read();
                            Console.WriteLine(data_rec);
                            break;
                        case "-6":
                            Serial_write("6");
                            data_rec = Serial_read();
                            Console.WriteLine(data_rec);
                            break;
                        case "-h":
                            Console.WriteLine();
                            Console.WriteLine("Insira COM PORT number como primeiro parametro, ex: COM1.\n");
                            Console.WriteLine("-0, seta rele START.");
                            Console.WriteLine("-1, seta rele STOP.");
                            Console.WriteLine("-2, Le resultado PASS.");
                            Console.WriteLine("-3, Le resultado FAIL.");
                            Console.WriteLine("-4, Verifica under test.");
                            Console.WriteLine("-5, Le entrada analógica A0");
                            Console.WriteLine("-6, Le entrada analógica A1");
                            Console.WriteLine("-h, lista funções");
                            Console.WriteLine("Functions: NRFail, PASS, ACHIGH, ACLOW, VND");
                            break;
                        default:
                            if (!aux.Contains("COM")) { Console.WriteLine("Nada recebido como parametro, use -h para ver as opções."); }
                            break;
                    }
                }
                Serial_closePort(args[0]);
            }
        }

        public static bool Serial_configAndOpen(string portNumber)
        {
            // Serial port configuration
            _serialPort = new SerialPort();
            _serialPort.PortName = portNumber;
            _serialPort.BaudRate = 9600;
            try
            {
                _serialPort.Open();
            }
            catch (Exception) { Console.WriteLine("Não é possivel abrir porta COM ->" + portNumber); return false; }
            // Set serial port timeout
            _serialPort.ReadTimeout = 2000;
            _serialPort.WriteTimeout = 1000;
            // defile whiel loop to send and receive data
            return true;
        }

        public static bool Serial_closePort(string portNumber)
        {
            try
            {
                _serialPort.Close();
                //Console.WriteLine("Porta COM fechada ->" + portNumber);
                return true;
            }catch (Exception) { Console.WriteLine("Não é possivel fechar porta COM ->" + portNumber); return false; }
        }

        public static string Serial_read()
        {
            while (_continue)
            {
                try
                {
                    string auxRead = _serialPort.ReadLine();
                    return auxRead;
                }catch (TimeoutException) { _continue = false; }
            }
            return "Falha nada recebido via COM!!";
        }

        public static bool Serial_write(string command)
        {
            try
            {
                _serialPort.Write(command);
                return true;
            }
            catch (TimeoutException) { _continue = false; return false; }
        }
    }
}
