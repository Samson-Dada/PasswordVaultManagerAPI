﻿using API.Shared.DataAccess;
using SharedUser =  API.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;
using API.Shared.Entities;

namespace API.Modules.User.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<SharedUser.User> _userManager;
        public UserRepository(UserManager<SharedUser.User> userManager)
        {
            _userManager = userManager;
        }


        public async  Task<IdentityResult> CreateUser(SharedUser.User user, string password)
        {
            IdentityResult newUser = await _userManager.CreateAsync(user, password);
            return newUser;
        }

        public async Task<IdentityResult> GetUserById(string id)
        {
            // Check if the user exists before attempting to delete
            SharedUser.User existingUser = await _userManager.FindByIdAsync(id);
            if (existingUser == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }
            IdentityResult deleteUser = await _userManager.DeleteAsync(existingUser);
            return deleteUser;
        }
        private async Task<IdentityUser> UserExist(string id)
        {
            var userId = await _userManager.FindByIdAsync(id);
            if (userId == null)
            {
                return null;
            }
            return userId;

        }


        public async Task<IdentityResult> UpdateUser(SharedUser.User user)
        {
            SharedUser.User existingUser = await _userManager.FindByIdAsync(user.Id);
            if (existingUser == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            return await _userManager.UpdateAsync(existingUser);
        }

        public async Task<bool> AlreadyExists(string userName)
        {
            IdentityUser userExist = await _userManager.FindByNameAsync(userName);
            return userExist != null;
        }


        // Delete a user
        public async Task<IdentityResult> DeleteUser(SharedUser.User user)
        {
            // Check if the user exists before attempting to delete
            SharedUser.User existingUser = await _userManager.FindByIdAsync(user.Id);
            if (existingUser == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            IdentityResult deleteUser = await _userManager.DeleteAsync(existingUser);
            return deleteUser;
        }

        /* OLD METHOD FUNCTIONALITY*/

        //public async Task<IEnumerable<SharedUser.User>> GetAll()
        //{
        //    var users = await _dbContext.AppUsers.Include(u => u.Password).ToListAsync();
        //    if (users is null)
        //    {
        //        return null;
        //    }
        //    return users;
        //}

        //public async Task Create(SharedUser.User user)
        //{
        //    //string salt = BCrypt.Net.BCrypt.GenerateSalt();
        //    //user.Password = BCrypt.Net.BCrypt.HashPassword(user., salt);

        //    await _dbContext.AppUsers.AddAsync(user);
        //    await _dbContext.SaveChangesAsync();
        //    await _dbContext.AppUsers.Include(u => u.Password).ToListAsync();
        //}

        //public async Task Delete(SharedUser.User user)
        //{
        //    _dbContext.AppUsers.Remove(user);
        //    await _dbContext.SaveChangesAsync();
        //    await Task.CompletedTask;
        //}




        //public async Task<SharedUser.User> GetById(string id)
        //{
        //    var userId = await _dbContext.AppUsers.Include(u => u.Password).SingleOrDefaultAsync(u => u.Id == id);
        //    if (userId is null)
        //    {
        //        return null;
        //    }
        //    return userId;
        //}

        //public async Task Update(SharedUser.User user)
        //{
        //    var existingUser = await _dbContext.AppUsers.Include(u => u.Password).SingleOrDefaultAsync(u => u.Id == user.Id);

        //    if (existingUser is null)
        //    {
        //        return;
        //    }

        //    existingUser.UserName = user.UserName;
        //    existingUser.Email = user.Email;

        //    var existingPassword = existingUser.Password.FirstOrDefault(p => p.Id == user.Password.FirstOrDefault()?.Id);
        //    if (existingPassword != null)
        //    {
        //        existingPassword.Title = user.Password.FirstOrDefault()?.Title;
        //        existingPassword.HashedPassword = user.Password.FirstOrDefault()?.HashedPassword;
        //    }
        //    else
        //    {
        //        return;
        //    }
        //    await _dbContext.SaveChangesAsync();
        //}

        //public async Task UpdateUser(string userId, string newUsername)
        //{
        //    var existingUser = await _dbContext.AppUsers.FindAsync(userId);

        //    if (existingUser is null)
        //    {
        //        return;
        //    }

        //    existingUser.UserName = newUsername;
        //    await _dbContext.SaveChangesAsync();
        //}


        //public async Task<bool> AlreadyExist(string userName)
        //{
        //    bool isExist = await _dbContext.AppUsers.Include(u => u.Password).AnyAsync(u => u.UserName == userName);
        //    return isExist;
        //}

        //public async Task<SharedUser.User> GetUserByName(string userName)
        //{

        //    var user = await _dbContext.AppUsers.SingleOrDefaultAsync(u => u.UserName == userName);
        //    //var user = await _dbContext.Users.Include(userName).FirstOrDefaultAsync(u => u.Username == userName);
        //    if (user is null)
        //    {
        //        return null;
        //    }
        //    return user;
        //}


        //public async Task<SharedUser.User> GetUserByNameWithPassword(string username)
        //{
        //    await GetUserByName(username);
        //    var user = await _dbContext.AppUsers
        //        .Include(u => u.Password)
        //        .Where(u => u.UserName == username)
        //        .FirstOrDefaultAsync();

        //    return user;
        //}

        //public async Task GetAllPassword(SharedUser.User user)
        //{
        //    await _dbContext.AppUsers.Include(p => p.Password).ToListAsync();
        //}

        // TODO::  Remove later

        //public async Task<IEnumerable<SharedUser.User>> GetAllUsers()
        // {
        //     var users = await _dbContext.AppUsers.ToListAsync();
        //     return users;
        // }
    }
}