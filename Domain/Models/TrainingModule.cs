using System;
using System.Collections.Generic;
using Domain.Interfaces;

namespace Domain.Models
{
    public class TrainingModule : ITrainingModule
    {
        private long _id;
        private Subject _assunto;
        private List<PeriodDateTime> _periodos;

        public TrainingModule(Subject assunto, List<PeriodDateTime> periodos)
        {
            _assunto = assunto;
            _periodos = periodos;
        }

        public TrainingModule(long id, Subject assunto, List<PeriodDateTime> periodos)
            : this(assunto, periodos){
                _id = id;

            }

        public long GetId()
        {
            return _id;
        }

        public void SetId(long id)
        {
            _id = id;
        }

        public Subject GetAssunto()
        {
            return _assunto;
        }

        public List<PeriodDateTime> GetPeriodos()
        {
            return _periodos;
        }

        public void SetPeriodos(List<PeriodDateTime> periodos)
        {
            _periodos = periodos;
        }
    }
}
