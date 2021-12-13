// using Castle.DynamicProxy;
// using System;
// using System.Text.Json;
// using System.Text.Json.Serialization;
// using Castle.Core.Interceptor;
// using Serilog;
// using IInterceptor = Microsoft.EntityFrameworkCore.Diagnostics.IInterceptor;
//
// namespace DataAPI.Logging
// {
//     public class MethodLog : Attribute, IInterceptor
//     {
//         public void Intercept(IInvocation invocation)
//         {
//             try
//             {
//                 string module = invocation.Method.DeclaringType.FullName;
//                 string method = invocation.Method.Name;
//                 JsonSerializerOptions options = new()
//                 {
//                     ReferenceHandler = ReferenceHandler.Preserve,
//                     WriteIndented = true
//                 };
//
//                 string args = GetArgsWithoutContext(invocation, options);
//
//                 Log.Logger.Information($"Calling method: {method} " +
//                                        $" in module: {module}\n" +
//                                        $"with args: {args}\n");
//
//
//                 invocation.Proceed();
//                 string returnValue = JsonSerializer.Serialize(invocation.ReturnValue, options);
//
//                 Log.Logger.Information($"Method finished successfully, returned: {returnValue}\n");
//                 
//             }
//             catch(Exception e)
//             {
//                 Log.Logger.Information($"Method failed, thrown exception: {JsonSerializer.Serialize(e)}\n");
//             }
//         }
//
//         private string GetArgsWithoutContext(IInvocation invocation, JsonSerializerOptions options)
//         {
//             string args = "";
//             foreach (var obj in invocation.Arguments)
//             {
//                 if (obj is not AppDbContext)
//                 {
//                     args += JsonSerializer.Serialize(obj, options) + ",\n";
//                 }
//             }
//             return "(" + args.Remove(args.Length - 1, 1) + ")";
//         }
//     }
// }