using System;
using Microsoft.EntityFrameworkCore;
using Psychology_API.Data;

namespace Psychology_XUnit.DataContextTest
{
    public class ConnectionFactory : IDisposable
    {
        private bool disposedValue = false;
        public DataContext CreateContextForInMemory()
        {
            var option = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "Test_Database").Options; 

            var context = new DataContext(option); 
            if (context != null)  
            {  
                context.Database.EnsureDeleted();  
                context.Database.EnsureCreated();  
            }  

            return context;
        }
        protected virtual void Dispose(bool disposing)  
        {  
            if (!disposedValue)  
            {  
                if (disposing)  
                {  
                }  
  
                disposedValue = true;  
            }  
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}