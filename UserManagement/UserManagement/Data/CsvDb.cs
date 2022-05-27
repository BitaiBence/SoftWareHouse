using UserManagement.Models;
using System.IO.IsolatedStorage;

namespace UserManagement.Data
{
        public class CsvDb
        {
            private string path;

            public CsvDb(string path)
            {
                this.path = path;
                IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();

                if (!isoStore.FileExists(path))
                {
                    //insert test data
                    using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(path, FileMode.Create, isoStore))
                    {
                        using (StreamWriter writer = new StreamWriter(isoStream))
                        {
                            writer.WriteLine($"0;test;password;test;test;{DateTime.Now};test;test");
                            writer.WriteLine($"1;John Doe;password123;Doe;John;{DateTime.Now};Budapest;Budapest");
                        }
                    }
                }
            }

            public async Task<List<User>> GetAllAsync()
            {
                return await ReadAsync();
            }

            public async Task<User?> FirstOrDefaultAsync(int? id)
            {
                List<User> users = await ReadAsync();
                return users.FirstOrDefault(u => u.Id == id);
            }

            public async Task<User?> FindAsync(int? id)
            {
                List<User> users = await ReadAsync();
                return users.Find(u => u.Id == id);
            }

            public async Task UpdateAsync(User user)
            {
                var users = await ReadAsync();
                int index = users.FindIndex(u => u.Id == user.Id);
                users[index] = user;
                await SaveChangesAsync(users);
            }

            public async Task AddAsync(User user)
            {
                var users = await ReadAsync();
                user.Id = users.Count + 1;

                await Task.Run(() =>
                {
                    IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();

                    using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(path, FileMode.Append, isoStore))
                    {
                        using (StreamWriter writer = new StreamWriter(isoStream))
                        {
                            writer.WriteLine($"{user.Id};{user.Name};{user.Password};{user.LastName};{user.FirstName};{user.BirthDate};{user.BirthPlace};{user.City}");
                        }
                    }
                });
            }

            public async Task RemoveAsync(User user)
            {
                var users = await ReadAsync();
                int index = users.FindIndex(u => u.Id == user.Id);
                users.RemoveAt(index);
                await SaveChangesAsync(users);
            }

            public async Task<List<User>> ReadAsync()
            {
                List<User> users = new List<User>();

                await Task.Run(() => {
                    IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();

                    if (isoStore.FileExists(path))
                    {
                        using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(path, FileMode.Open, isoStore))
                        {
                            using (StreamReader reader = new StreamReader(isoStream))
                            {
                                while (!reader.EndOfStream)
                                {
                                    string[] line = reader.ReadLine().Split(';');
                                    users.Add(new User(Convert.ToInt32(line[0]), line[1], line[2], line[3], line[4], Convert.ToDateTime(line[5]), line[6], line[7]));
                                }
                            }
                        }
                    }
                });

                return users;
            }

            public async Task<bool> AnyAsync(int id)
            {
                List<User> users = await ReadAsync();
                return users.Any(u => u.Id == id);
            }

            public async Task SaveChangesAsync(List<User> users)
            {
                await WriteAsync(users);
            }

            public async Task WriteAsync(List<User> users)
            {
                await Task.Run(() =>
                {
                    IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();

                    using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(path, FileMode.Create, isoStore))
                    {
                        using (StreamWriter writer = new StreamWriter(isoStream))
                        {
                            foreach (var user in users)
                            {
                                writer.WriteLine($"{user.Id};{user.Name};{user.Password};{user.LastName};{user.FirstName};{user.BirthDate};{user.BirthPlace};{user.City}");
                            }
                        }
                    }
                });
            }

            public void InitDatabase()
            {
                IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();

                using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(path, FileMode.Create, isoStore))
                {
                    using (StreamWriter writer = new StreamWriter(isoStream))
                    {

                    }
                }
            }
        }

}
