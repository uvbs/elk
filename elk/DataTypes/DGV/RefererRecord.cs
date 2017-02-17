namespace Elk.DataTypes.Dgv
{
  using System.ComponentModel;


  public class RefererRecord : INotifyPropertyChanged
  {

    #region MEMBERS
    
    private string entryHostName;
    private string endHostName;
    private string cookies;

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion


    #region PUBLIC

    public RefererRecord(string entryHostName, string endHostName, string cookies)
    {
      this.entryHostName = entryHostName;
      this.endHostName = endHostName;
      this.cookies = cookies;
    }

    #endregion


    #region PROPERTIES

    [Browsable(true)]
    public string EntryHostName
    {
      get { return this.entryHostName; }
      set
      {
        this.entryHostName = value;
        this.NotifyPropertyChanged("EntryHostName");
      }
    }

    [Browsable(true)]
    public string EndHostName
    {
      get { return this.endHostName; }
      set
      {
        this.endHostName = value;
        this.NotifyPropertyChanged("EndHostName");
      }
    }


    [Browsable(true)]
    public string Cookies
    {
      get { return this.cookies; }
      set
      {
        this.cookies = value;
        this.NotifyPropertyChanged("Cookies");
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
