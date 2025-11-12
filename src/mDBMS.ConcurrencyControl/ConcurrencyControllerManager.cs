using mDBMS.Common.Interfaces;
using mDBMS.Common.Models;
using System.Threading;

namespace mDBMS.ConcurrencyControl
{
    public class ConcurrencyControlManager : IConcurrencyControl
    {
        private static int _txCounter = 0;
        public int begin_transaction()
        {
            // Tulis pesan debug ke konsol
            int id = Interlocked.Increment(ref _txCounter);
            Console.WriteLine("[MOCK CCM]: begin_transaction() dipanggil. Task Id={id}");
            // Kembalikan ID transaksi palsu
            return id;
        }
    }   
}