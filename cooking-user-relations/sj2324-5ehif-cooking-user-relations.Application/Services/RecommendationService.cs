using System.Threading.Tasks;
using sj2324_5ehif_cooking_user_relations.Application.DTO.Follow;
using sj2324_5ehif_cooking_user_relations.Application.DTO.User;
using sj2324_5ehif_cooking_user_relations.Application.DTO.Recipe;
using sj2324_5ehif_cooking_user_relations.Application.Model;
using sj2324_5ehif_cooking_user_relations.Application.Repository;

namespace sj2324_5ehif_cooking_user_relations.Application.Services
{
    public interface IRecommendationSerivce
    {
        Task<double> AverageRating(RecipeDto recipe);
        Task<int> TotalShares(RecipeDto recipe);
        Task<int> TotalFollowers(UserDto user);
        Task<int> TotalFollowing(UserDto user);
        Task<int> TotalRecipes(UserDto user);
        Task<RecipeDto> Recommend(UserDto user);
        Task<double> AverageUserRating(UserDto user);
        Task<double> AverageUserRatingGiven(UserDto user);
        Task<IEnumerable<UserDto>> Followers(UserDto user);
        Task<IEnumerable<UserDto>> Following(UserDto user);
        Task<IEnumerable<RecipeDto>> RecipesPosted(UserDto user);
        Task<IEnumerable<RecipeDto>> RecommendList(UserDto user);
    }

    public class RecommendationService : IRecommendationSerivce
    {
        private readonly IRepository<Follow> _followRepository;
        private readonly IRepository<Feedback> _feedbackRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Recipe> _recipeRepository;
        private readonly IRepository<RecipeShare> _recipeShareRepository;

        public RecommendationService()
        {

        }

        public RecommendationService(
            IRepository<Follow> followRepository, 
            IRepository<Feedback> feedbackRepository,
            IRepository<User> userRepository, 
            IRepository<Recipe> recipeRepository,
            IRepository<RecipeShare> recipeShareRepository
        )
        {
            _followRepository = followRepository;
            _feedbackRepository = feedbackRepository;
            _userRepository = userRepository;
            _recipeRepository = recipeRepository;
            _recipeShareRepository = recipeShareRepository;
        }


        public async Task<double> AverageRating(RecipeDto recipe)
        {
            Key recipeKey = recipe.Key;
            List<Feedback> list = await _feedbackRepository.GetAllAsync().Result.entity;

            if (list is null)
            {
                return 0d;
            }

            list = list.Where(
                x => x.Recipe.Key == recipeKey
            ).ToList();
            int n = list.Count;
            if (n == 0)
            {
                return 0d;
            }
            double a = 0d;
            for (int i = 0; i < n; i++)
            {
                a += list[i].Rating;
            }
            return a / n;
        }

        public async Task<int> TotalShares(RecipeDto recipe)
        {
            Key recipeKey = recipe.Key;
            List<RecipeShare> list = await _recipeShareRepository.GetAllAsync().Result.entity;
            
            if (list is null)
            {
                return 0;
            }

            list = list.Where(
                x => x.Recipe.Key == recipeKey
            );
            return list.Count;
        }

        public async Task<int> Followers(UserDto user)
        {
            Key userKey = user.Key;
            List<Follow> list = await _followRepository.GetAllAsync().Result.entity;

            if (list is null)
            {
                return 0;
            }

            list = list.Where(
                x => x.User.Key == userKey
            );
            return list.Count;
        }

        public async Task<int> Following(UserDto user)
        {
            Key userKey = user.Key;
            List<Follow> list = await _followRepository.GetAllAsync().Result.entity;

            if (list is null)
            {
                return 0;
            }

            list = list.Where(
                x => x.Follower.Key == userKey
            );
            return list.Count;
        }

        public async Task<int> TotalRecipes(UserDto user)
        {
            Key userKey = user.Key;
            List<Recipe> list = await _recipeRepository.GetAllAsync().Result.entity;

            if (list is null)
            {
                return 0;
            }
            list = list.Where(
                x => x.AuthorKey == userKey    
            );
            return list.Count;
        }

        public Task<IEnumerable<UserDto>> Followers(UserDto user)
        {
            Key userKey = user.Key;
            List<Follow> list = await _followRepository.GetAllAsync().Result.entity;

            if (list is null)
            {
                return 0;
            }

            list = list.Where(
                x => x.User.Key == userKey
            );

            var userDtos = list.Select(f => new UserDto
            {
                Key = f.Follower.Key,
                Name = f.Follower.Name
            });
            return userDtos;
        }

        public Task<IEnumerable<UserDto>> Following(UserDto user)
        {
            Key userKey = user.Key;
            List<Follow> list = await _followRepository.GetAllAsync().Result.entity;

            if (list is null)
            {
                return 0;
            }

            list = list.Where(
                x => x.Follower.Key == userKey
            );

            var userDtos = list.Select(f => new UserDto
            {
                Key = f.User.Key,
                Name = f.User.Name
            });
            return userDtos;
        }

        public Task<IEnumerable<RecipeDto>> RecipesPosted(UserDto user)
        {
            Key userKey = user.Key;
            List<Recipe> list = await _recipeRepository.GetAllAsync().Result.entity;

            if (list is null)
            {
                return 0;
            }

            list = list.Where(
                x => x.AuthorKey == userKey
            );

            var recipeDtos = list.Select(r => new RecipeDto
            {
                Key = r.Key,
                Name = r.Name,
                AuthorKey = userKey
            });
            return recipeDtos;
        }

        public async Task<double> AverageUserRating(UserDto user)
        {
            Key userKey = user.Key;
            List<Recipe> recipes = await RecipesPosted(user).Result;

            if (recipes is null)
            {
                return 0d;
            }
            var recipesDto = list.Select(r => new RecipeDto
            {
                Key = r.Key,
                Name = r.Name,
                AuthorKey = userKey
            });
            double a = 0d;
            for (int i = 0; i < recipesDto.Count; i++)
            {
                a += AverageRating(recipesDto[i]);
            }
            return a / recipesDto.Count;
        }

        public Task<double> AverageUserRatingGiven(UserDto user)
        {
            Key userKey = user.Key;
            List<Feedback> list = _feedbackRepository.GetAllAsync().Result.entity;

            if (list is null)
            {
                return 0d;
            }

            list = list.Where(
                x => x.From.Key == userKey
            );
            
            double a = 0d;
            for (int i = 0; i < list.Count; i++)
            {
                a += list[i].Rating;
            }
            return a / list.Count;
        }



        private static bool[][] stpSet;
        private static int[][] distance;

        public int GetData()
        {
            return new Random().Next();
        }

        
        public void Recommend()
        {
            int[][] matrix = new int[][];
            stpSet = new bool[][];
            distance = new int[][];
            int[][][] graph = new int[][][];
            for (int i = 0; i < matrix.Length; i++)
            {
                distance[i] = new int[];
                graph[i] = new int[][];
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    distance[i][j] = int.MaxValue;
                    int[] cost = new int[4];
                    if (i == 0) cost[0] = 0;
                    else cost[0] = matrix[i - 1][j];
                    if (j == matrix[i].Length - 1) cost[1] = 0;
                    else cost[1] = matrix[i][j + 1];
                    if (i == matrix.Length - 1) cost[2] = 0;
                    else cost[2] = matrix[i + 1][j];
                    if (j == 0) cost[3] = 0;
                    else cost[3] = matrix[i][j - 1];
                    graph[i][j] = cost;
                }
            }
            distance[0][0] = matrix[0][0];
            for (int i = 0; i < graph.Length; i++)
            {
                for (int j = 0; j < graph[i].Length; j++)
                {
                    int[] u = d();
                    stpSet[u[0]][u[1]] = true;

                    for (int k = 0; k < graph[i][j].Length; k++)
                    {
                        int i1 = (k == 0 ? -1 : k == 2 ? 1 : 0) + u[0];
                        int j1 = (k == 1 ? 1 : k == 3 ? -1 : 0) + u[1];
                        if (i1 < 0 || i1 > 79 || j1 < 0 || j1 > 79) continue;
                        int du = distance[u[0]][u[1]];
                        int cost = graph[u[0]][u[1]][k];
                        if (cost != 0 && !stpSet[i1][j1] && cost + du < distance[i1][j1])
                        {
                            distance[i1][j1] = distance[u[0]][u[1]] + cost;
                        }
                    }
                }
            }
        }

        public int[] d()
        {
            int min = int.MaxValue;
            int[] index = new int[2];
            for (int i = 0; i < 80; i++)
            {
                for (int j = 0; j < 80; j++)
                {
                    if (stpSet[i][j] == false && distance[i][j] < min)
                    {
                        min = distance[i][j];
                        index[0] = i;
                        index[1] = j;
                    }
                }
            }
            return index;
        }
    }
}