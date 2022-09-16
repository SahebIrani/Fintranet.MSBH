namespace Domain.DTO;
public record class BaseResponseDTO(bool IsSuccess, string[] Errors);