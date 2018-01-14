﻿/*************************************************************************/
/*  ExceptionsEvents.cs                                                  */
/*************************************************************************/
/*                       This file is part of:                           */
/*                              WebPlex                                  */
/*************************************************************************/
/* Copyright (c) 2017-2017 Badr Azizi.                                   */
/*                                                                       */
/* Permission is hereby granted, free of charge, to any person obtaining */
/* a copy of this software and associated documentation files (the       */
/* "Software"), to deal in the Software without restriction, including   */
/* without limitation the rights to use, copy, modify, merge, publish,   */
/* distribute, sublicense, and/or sell copies of the Software, and to    */
/* permit persons to whom the Software is furnished to do so, subject to */
/* the following conditions:                                             */
/*                                                                       */
/* The above copyright notice and this permission notice shall be        */
/* included in all copies or substantial portions of the Software.       */
/*                                                                       */
/* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,       */
/* EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF    */
/* MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.*/
/* IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY  */
/* CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,  */
/* TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE     */
/* SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.                */
/*************************************************************************/

using System;
using System.IO;
using System.Threading;

namespace WebPlex.ExceptionHandler
{
    public static class Logger
    {
        public static void log(string text)
        {
            Console.WriteLine("[" + DateTime.Now.ToString() + "]\t" + text);
            if (File.Exists("log/log.txt"))
            {
                using (StreamWriter log = File.AppendText("log/log.txt"))
                {
                    log.WriteLine("[" + DateTime.Now.ToString() + "]\t" + text);
                    log.Flush();
                }
            }
            else
            {
                if (!Directory.Exists("log"))
                {
                    Directory.CreateDirectory("log");
                    Thread.Sleep(1000);
                }
                using (StreamWriter log = File.AppendText("log/log.txt"))
                {
                    log.WriteLine("[" + DateTime.Now.ToString() + "]\t" + text);
                    log.Flush();
                }
            }
        }
    }
}
