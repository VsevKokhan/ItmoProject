using Models.DTO;
using Models.Model;

namespace Interfaces;

public interface IUserService
{
    public UserEntity Add(UserDto user);
    public UserEntity? Get(int id);
    public UserEntity? Get(string pas, string name);
}