namespace Elk.Task
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;


  public class TcpPortChecker
  {

    public bool IsPortOpen(string server, int port, string message)
    {
      //string message = "GET / HTTP/1.1\n\n";
      System.Net.Sockets.NetworkStream stream = null;
      System.Net.Sockets.TcpClient client = null;

      try
      {
        client = new System.Net.Sockets.TcpClient(server, port);
        client.ReceiveTimeout = 3000;
        //client.SendTimeout = 3000;

        // Translate the passed message into ASCII and store it as a Byte array.
        Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

        // Get a client stream for reading and writing.
        stream = client.GetStream();

        // Send the message to the connected TcpServer. 
        stream.Write(data, 0, data.Length);

        // Buffer to store the response bytes.
        data = new Byte[256];
        // String to store the response ASCII representation.
        String responseData = String.Empty;

        // Read the first batch of the TcpServer response bytes.
        Int32 bytes = stream.Read(data, 0, data.Length);
        responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

        if (string.IsNullOrEmpty(responseData) )
        {
          return false;
        }
        //else if (!responseData.StartsWith("HTTP/1."))
        //{
        //  return false;
        //}
        else
        {
          return true;
        }
      }
      catch (ArgumentNullException)
      {
      }
      catch (System.Net.Sockets.SocketException)
      {
      }
      catch (Exception)
      {        
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
