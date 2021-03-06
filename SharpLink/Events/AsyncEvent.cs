﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpLink.Events
{
    public delegate Task AsyncEvent<in T1>(T1 arg1);
    public delegate Task AsyncEvent<in T1, in T2>(T1 arg1, T2 arg2);
    public delegate Task AsyncEvent<in T1, in T2, in T3>(T1 arg1, T2 arg2, T3 arg3);

    public static class AsyncEventExtensions
    {
        public static Task InvokeAsync<T1>(this AsyncEvent<T1> asyncEvent, T1 arg1)
        {
            if (asyncEvent == null)
                return Task.CompletedTask;

            IEnumerable<AsyncEvent<T1>> events = asyncEvent.GetInvocationList().Cast<AsyncEvent<T1>>();
            IEnumerable<Task> eventTasks = events.Select(it => it.Invoke(arg1));

            return Task.WhenAll(eventTasks);
        }

        public static Task InvokeAsync<T1, T2>(this AsyncEvent<T1, T2> asyncEvent, T1 arg1, T2 arg2)
        {
            if (asyncEvent == null)
                return Task.CompletedTask;

            IEnumerable<AsyncEvent<T1, T2>> events = asyncEvent.GetInvocationList().Cast<AsyncEvent<T1, T2>>();
            IEnumerable<Task> eventTasks = events.Select(it => it.Invoke(arg1, arg2));

            return Task.WhenAll(eventTasks);
        }

        public static Task InvokeAsync<T1, T2, T3>(this AsyncEvent<T1, T2, T3> asyncEvent, T1 arg1, T2 arg2, T3 arg3)
        {
            if (asyncEvent == null)
                return Task.CompletedTask;

            IEnumerable<AsyncEvent<T1, T2, T3>> events = asyncEvent.GetInvocationList().Cast<AsyncEvent<T1, T2, T3>>();
            IEnumerable<Task> eventTasks = events.Select(it => it.Invoke(arg1, arg2, arg3));

            return Task.WhenAll(eventTasks);
        }
    }
}
