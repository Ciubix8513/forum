//The GPLv3 License (GPLv3)
//
//Copyright (c) 2023 Ciubix8513
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
using Bwasm.Cookies.Models;
using Api.Dtos;

namespace Bwasm.Cookies.Logic
{
    public interface IApiLogic
    {
        Task<string> LoginAsync(LoginModel login);
        Task<string> LogoutAsync();
        Task<(string Message,UserProfileModel? userProfile)> UserProfileAsync();
        Task<List<PostsGetDto>> GetPosts(int parent);
        Task<PostsGetDto> GetPost(int Id);
        Task<string> AddForm(FormAddDto dto); 
        Task RepPost(int id, string reason);
        Task RepUser(int id, string reason);
        Task AddPost(string contents, int parentId);
        Task DeletePost(int id);        
        Task EditPost(int id,string content);        
        Task<List<PostsGetDto>> GetPostsUser(int Id);
        Task EditBio(int id,string content);
        Task<UserDataDto> GetUser(int id);
        Task<List<FormGetDto>> GetForms();
        Task DeleteForm(int id);
        Task AddUser(int id);
        Task<List<RepDto>> GetPostReports();
        Task<List<RepDto>> GetUserReports();
        Task DeletePostReport(int id);
        Task DeleteUserReport(int id);
    }
}