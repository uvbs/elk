namespace Elk.DataTypes
{
  using Elk.DataTypes.Enumerations;


  public class VulnerableElementAndConsequences
  {

    #region MEMBERS

    private EnumExposedDataType exposedDataTypes;
    private EnumRequestTriggerSource requestTriggeredBy;

    #endregion


    #region PROPERTIES
    
    public EnumExposedDataType ExposedDataTypes { get { return this.exposedDataTypes; } set { } }

    public EnumRequestTriggerSource RequestTriggeredBy { get { return this.requestTriggeredBy;  } set { } }

    #endregion


    #region PUBLIC

    public VulnerableElementAndConsequences(EnumRequestTriggerSource requestTriggeredBy, EnumExposedDataType exposedDataTypes)
    {
      this.requestTriggeredBy = requestTriggeredBy;
      this.exposedDataTypes = exposedDataTypes;
    }

    #endregion

  }
}
