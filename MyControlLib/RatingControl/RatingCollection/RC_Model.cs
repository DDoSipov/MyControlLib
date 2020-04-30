using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace MyControlLib.RatingControl.RatingCollection
{
    /// <summary>
    /// Rating Collection Model.
    /// </summary>
    public class RC_Model
    {
        private double _Value;

        private readonly Collection<RatingElement.RE_Model> _RE_Models;
        private readonly RC_Model_Update_Service _Updater;


        public RC_Model(IList<RatingElement.RE_Model> models, double startValue = 0)
        {
            _RE_Models = new Collection<RatingElement.RE_Model>(models);
            RE_Models = new ReadOnlyCollection<RatingElement.RE_Model>(_RE_Models);
            _Updater = new RC_Model_Update_Service();

            Value = startValue;
        }


        public double Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                _Updater.Update(_Value, RE_Models);
            }
        }
        public ReadOnlyCollection<RatingElement.RE_Model> RE_Models { get; }
    }


    /// <summary>
    /// Logic to update collection of models.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="re_Models"></param>
    public class RC_Model_Update_Service
    {
        public void Update(double new_Value, ReadOnlyCollection<RatingElement.RE_Model> RE_Models)
        {
            int count = RE_Models.Count;

            double Step = 1.0 / count;
            double ival = 0;


            for (int i = 0; i < count; i++)
            {
                if (ival + Step <= new_Value)
                    RE_Models[i].Value = 1;
                else
                {
                    RE_Models[i].Value = (new_Value - ival) / Step;
                    for (int j = i + 1; j < count; j++)
                        RE_Models[j].Value = 0;
                    break;
                }

                ival += Step;
            }
        }
    }
}
