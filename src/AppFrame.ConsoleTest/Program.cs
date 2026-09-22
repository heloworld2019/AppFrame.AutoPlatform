using AppFrame.Common.Extensions.ConcurrentBag;
using AppFrame.ConsoleTest.Interfaces;
using AppFrame.ConsoleTest.Repository;
using AppFrame.Core.AppFrameLogger;
using AppFrame.Infrastructrue.DependencyInjection;
using SqlSugar;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AppFrame.ConsoleTest
{
    internal class Program
    {
        private static AppIoc _appIoc;
        static void Main(string[] args)
        {
            _appIoc = new AppIoc();
            //LoggerTest();
            //TryTake();
            //ConfigLoadAndSaveTest();
            //ConfigSaveTest();
            //UShortTest();
            //InterfaceTest();
            //RepositoryTest();
            //MaxTest();
            HexTest();
            MinHexTest();
            Console.WriteLine("按任意键退出...");
            Console.ReadKey();
        }
        static void LoggerTest()
        {
            IAppLogger logger = _appIoc.Resolve<IAppLogger>();
            logger.Debug("测试debug日志");
            logger.Info("测试Info日志");
            logger.Warn("测试Warn日志");
            logger.Error("测试Error日志");
        }
        static void TryTake()
        {
            var stu1 = new Student(1, "app1");
            var students = new ConcurrentBag<Student>
            {
                new Student(2, "app2"),
                new Student(3, "app3"),
                stu1
            };
            students.Remove(stu1);
            foreach (var item in students)
            {
                Console.WriteLine(item.Name);
            }
        }
        static void ConfigLoadAndSaveTest()
        {
            var config = new ConfigurationTest(_appIoc);
            config.Load();
            config.Save(false);
            //config.RenameConfig("J00-522", "J00-523");
            //config.RemoveConfig("J00-523");
            //config.CreateConfig("J00-522");
        }
        static void ConfigSaveTest()
        {
            var config = new ConfigurationTest(_appIoc);
            config.Save(true);
        }
        static void UShortTest()
        {
            ushort[] cards = new ushort[] { 0, 1, 2, 3, 4, 5, 6 };
            var model = cards.Any(p => p == 2);
            Console.WriteLine(model);
        }
        static void InterfaceTest()
        {
            List<IAnimal> animals = new List<IAnimal>
            {
                new Dog(),
                new Dog(),
                new Dog(),
            };
            foreach (var item in animals)
            {
                var animal = (Dog)item;
                Console.WriteLine(animal.Name);
            }
        }
        static void RepositoryTest()
        {
            var repository = new RepositoryTest(_appIoc);
            //repository.Insert();
            //repository.AddUser();
            repository.Query();
        }
        static void MaxTest()
        {
            DateTime date = new DateTime(2022, 11, 1);
            var datas = new List<Data>();
            for (int i = 0; i < 10; i++)
            {
                datas.Add(new Data(date.AddDays(i).ToString("yyyy-MM-dd"), $"data{i}", i));
            }
            var res2 = datas.FirstOrDefault(p => p.Date.Equals(datas.Max(x => x.Date)));
        }
        static void HexTest()
        {
            string hex = "1A00";
            int value = Convert.ToInt32(hex, 16);
            Console.WriteLine(value);
        }
        static void MinHexTest()
        {
            var list = new List<string>() { "16#1A00", "16#1A01", "16#1A10", "16#1A11", "16#1A20", "16#1A21" };
            Console.WriteLine(list.Min());
        }
    }
    public class Data
    {
        public Data(string date, string name, int id)
        {
            Date = date;
            Name = name;
            Id = id;
        }

        public string Date { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
