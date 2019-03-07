using APIErrorHandling;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.ExceptionHandeling
{
    public class CustomExceptionHandeling
    {
        public async Task<T> TryCatch<R, T>(Func<Task<T>> function) where R : Exception
        {
            try
            {
                return await function();
            }
            catch (R ex)
            {                
                //if (ex is ArgumentNullException)
                //{
                return await Task.FromResult((T)Activator.CreateInstance(typeof(T)));
                //}

            }
            //Last Exception in the order
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception thrown: {ex.Message}");

                throw new Exception(ex.Message);
            }
        }
    }
}
