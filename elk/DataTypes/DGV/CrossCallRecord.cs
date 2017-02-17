namespace Elk.DataTypes.Dgv
{
  using System.ComponentModel;


  public class CrossCallRecord : INotifyPropertyChanged
  {

    #region MEMBERS

    private string tag;
    private string scheme;
    private string hostName;
    private string path;

    private string outerHtml;
    private int streamPosition;
    private int line;
    private int linePosition;

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion


    #region PUBLIC

    public CrossCallRecord(string tag, string scheme, string hostName, string path)
    {
      this.tag = tag;
      this.scheme = scheme;
      this.hostName = hostName;
      this.path = path;

      this.outerHtml = string.Empty;
      this.streamPosition = 0;
      this.line = 0;
      this.linePosition = 0;
    }

    #endregion


    #region PROPERTIES

    [Browsable(true)]
    public string Tag
    {
      get { return this.tag; }
      set
      {
        this.tag = value;
        this.NotifyPropertyChanged("Tag");
      }
    }

    [Browsable(true)]
    public string Scheme
    {
      get { return this.scheme; }
      set
      {
        this.scheme = value;
        this.NotifyPropertyChanged("Scheme");
      }
    }

    [Browsable(true)]
    public string HostName
    {
      get { return this.hostName; }
      set
      {
        this.hostName = value;
        this.NotifyPropertyChanged("HostName");
      }
    }


    [Browsable(true)]
    public string Path
    {
      get { return this.path; }
      set
      {
        this.path = value;
        this.NotifyPropertyChanged("Path");
      }
    }


    [Browsable(true)]
    public string OuterHtml
    {
      get { return this.outerHtml; }
      set
      {
        this.outerHtml = value;
        this.NotifyPropertyChanged("OuterHtml");
      }
    }


    [Browsable(true)]
    public int StreamPosition
    {
      get { return this.streamPosition; }
      set
      {
        this.streamPosition = value;
      }
    }


    [Browsable(true)]
    public int Line
    {
      get { return this.line; }
      set
      {
        this.line = value;
      }
    }


    [Browsable(true)]
    public int LinePosition
    {
      get { return this.linePosition; }
      set
      {
        this.linePosition = value;
      }
    }

    #endregion


    #region PRIVATE

    /// <summary>
    ///
    /// </summary>
    /// <param name="name"></param>
    private void NotifyPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    #endregion

  }
}
