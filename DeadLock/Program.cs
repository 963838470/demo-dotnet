﻿using common;
using DeadLock.DAL;
using DeadLock.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;

namespace DeadLock
{
    /// <summary>
    /// 测试和解决死锁
    /// </summary>
    class Program
    {
        private static ADAL _ADAL = new ADAL();
        private static BDAL _BDAL = new BDAL();
        private static Random random = new Random();
        private static int num = 0;

        static void Main(string[] args)
        {
            InitData();

            for (int i = 0; i < 1000; i++)
            {
                Thread thread = new Thread(t1);
                thread.Start();
                Thread thread2 = new Thread(t2);
                thread2.Start();
            }

            Console.ReadLine();
        }

        static void Tran01()
        {
            _ADAL.Delete(2);
            _BDAL.Delete(2);
            string sql = @"begin tran
                           begin try
                             insert into A values (2,1)
                             insert into B values (2,null)
                           end try
                           begin catch
                              if @@trancount > 0
                                 rollback tran
                           end catch

                           if @@trancount > 0
                              commit tran
                           go";
            var value = SqlHelper.ExecuteNonQuery(sql);
            Console.WriteLine(value);
        }

        static void Tran02()
        {
            using (SqlTransaction trans = SqlHelper.BeginTransaction())
            {
                try
                {
                    _ADAL.Delete(2);
                    _BDAL.Delete(2);
                    _ADAL.Insert(new AInfo() { ID = 2, Value = 10086 }, trans);
                    _BDAL.Insert(new BInfo() { ID = 2, Value = 1111111111 }, trans);
                    trans.Commit();
                    Console.WriteLine("事务2执行成功");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    trans.Rollback();
                }
            }
        }

        static void InitData()
        {
            _ADAL.Delete(1);
            _ADAL.Insert(new AInfo() { ID = 1, Value = 0 });
            _BDAL.Delete(1);
            _BDAL.Insert(new BInfo() { ID = 1, Value = 0 });
        }

        static void t1()
        {
            using (SqlTransaction trans = SqlHelper.BeginTransaction())
            {
                try
                {
                    _ADAL.Update(new AInfo() { ID = 1, Value = 1 }, trans);
                    Thread.Sleep(random.Next(0, 100));
                    _BDAL.Update(new BInfo() { ID = 1, Value = 1 }, trans);
                    trans.Commit();
                    Console.WriteLine("t1执行成功:" + ++num);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    trans.Rollback();
                }
            }
        }


        static void t2()
        {
            using (SqlTransaction trans = SqlHelper.BeginTransaction())
            {
                try
                {
                    _ADAL.Update(new AInfo() { ID = 1, Value = 2 }, trans);
                    Thread.Sleep(random.Next(0, 100));
                    _BDAL.Update(new BInfo() { ID = 1, Value = 2 }, trans);
                    trans.Commit();
                    Console.WriteLine("t2执行成功:" + ++num);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    trans.Rollback();
                }
            }
        }
    }
}
