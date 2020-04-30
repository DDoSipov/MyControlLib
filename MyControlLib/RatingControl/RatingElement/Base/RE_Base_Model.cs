namespace MyControlLib.RatingControl.RatingElement.Base
{
    public interface IRE_Base_Model
    {
        double Value { get; set; }
        object Data { get; }

        /// <summary>
        /// Get deep copy of model
        /// </summary>
        /// <returns></returns>
        IRE_Base_Model GetCopy();
    }
}
