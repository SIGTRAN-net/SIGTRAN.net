/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Network.IP.IPv4.Controllers;

internal sealed partial class IPv4Controller : IDisposable
{
    private bool disposedValue;

    private void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                this.socket.Close();
                if (this.socketListeningThread is not null)
                    this.socketListeningThread = null;
            }
            disposedValue = true;
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
