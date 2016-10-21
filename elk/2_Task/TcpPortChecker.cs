namespace Elk.Task
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;


  public class TcpPortChecker
  {

    public bool IsPortOpen(string server, int port)
    {
      //string message = "GET / HTTP/1.1\n\n";
      System.Net.Sockets.NetworkStream stream = null;
      System.Net.Sockets.TcpClient client = null;

      try
      {
        client = new System.Net.Sockets.TcpClient();
        var result = client.BeginConnect(server, port, null, null);

        var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(3));

        if (!success)
        {
          return false;
        }

        // we have connected
        client.EndConnect(result);

        return true;
      }
      catch (ArgumentNullException)
      {
        return false;
      }
      catch (System.Net.Sockets.SocketException)
      {
        return false;
      }
      catch (Exception)
      {
        return false;
      }
      finally
      {
        // Close everything.
        if (stream != null)
        {
          stream.Close();
        }

        if (client != null)
        {
          client.Close();
        }
      }

      return false;
    }
  }
}
