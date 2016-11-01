namespace Elk.DataTypes
{
  public class UserAgent
  {

    #region PROPERTIES 

    public string Name { get; set; }

    public string Value { get; set; }

    #endregion


    #region PUBLIC 

    public UserAgent(string name, string value)
    {
      this.Name = name;
      this.Value = value;
    }

    #endregion

  }
}
