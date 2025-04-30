using Domain.Models;

namespace Application.DTO
{
    public record CollaboratorDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public PeriodDateTime PeriodDateTime { get; set; }

        public CollaboratorDTO(Guid id, Guid userId, PeriodDateTime periodDateTime)
        {
            Id = id;
            UserId = userId;
            PeriodDateTime = periodDateTime;
        }

        static public CollaboratorDTO ToDTO(Collaborator colab) {

		CollaboratorDTO colabDTO = new CollaboratorDTO(colab.Id,colab.UserId,colab.PeriodDateTime );

		return colabDTO;
	}

	static public IEnumerable<CollaboratorDTO> ToDTO(IEnumerable<Collaborator> colabs)
	{
		List<CollaboratorDTO> colabsDTO = new List<CollaboratorDTO>();

		foreach( Collaborator colab in colabs ) {
			CollaboratorDTO colabDTO = ToDTO(colab);

			colabsDTO.Add(colabDTO);
		}

		return colabsDTO;
	}

	static public Collaborator ToDomain(CollaboratorDTO colabDTO) {
		
		Collaborator colab = new Collaborator(colabDTO.Id,colabDTO.UserId,colabDTO.PeriodDateTime);

		return colab;
	}

    }
}
