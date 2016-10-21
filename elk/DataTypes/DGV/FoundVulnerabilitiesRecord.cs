namespace Elk.DataTypes.DGV
{
  using System.ComponentModel;


  public class FoundVulnerabilitiesRecord : INotifyPropertyChanged
  {

    #region MEMBERS

    private string title;
    private string description;

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion


    #region PROPERTIES

    [Browsable(true)]
    public string Title
    {
      get { return this.title; }
      set
      {
        this.title = value;
        this.NotifyPropertyChanged("Title");
      }
    }

    [Browsable(true)]
    public string Description
    {
      get { return this.description; }
      set
      {
        this.description = value;
        this.NotifyPropertyChanged("Description");
      }
    }

    #endregion


    #region PUBLIC

    public FoundVulnerabilitiesRecord(string title, string description)
    {
      this.title = title;
      this.description = description;
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
