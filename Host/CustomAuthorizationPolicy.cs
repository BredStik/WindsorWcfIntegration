using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    public class CustomAuthorizationPolicy: IAuthorizationPolicy
    {
        public string Id
        {
            get { return Guid.NewGuid().ToString(); }
        }

        private static IIdentity GetClientIdentity(EvaluationContext evaluationContext)
        {
            object obj;
            if (!evaluationContext.Properties.TryGetValue("Identities", out obj))
                throw new Exception("No Identity found");

            var identities = obj as IList<IIdentity>;
            if (identities == null || identities.Count <= 0)
                throw new Exception("No Identity found");

            return identities.First();
        }

        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            // set a new principal holding the combined roles
            // this could be your own IPrincipal implementation
            var currentIdentity = GetClientIdentity(evaluationContext);
            evaluationContext.Properties["Principal"] = new GenericPrincipal(new GenericIdentity(currentIdentity.Name + "_Modified"), new string[]{});

            return true;
        }

        public ClaimSet Issuer
        {
            get { return ClaimSet.System; }
        }
    }
}
