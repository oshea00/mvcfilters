## Dependency injection and filters

Since AspNetCore Filters are implemented as Attributes, they can only have a parameterless constructor.
This means you cannot use constructor-based dependency injection with Filters. (unless you
swap out the default IOC container in AspNetCore - probably a bad idea if the only reason
is this problem.)

There are other less invasive workarounds to this problem:
1. You can call the service provider yourself (HttpContext.RequestServices.GetService)
- Simplest approach, but hides dependencies and is a code smell.
2. You can use the ServiceFilter: [ServiceFilter(typeof(MyFilter))] 
- Allows constructor on MyFilter to pass in dependencies.
- Drawback: there's no way to easily configure filter behavior on a case-by-case basis.
3. You can use a TypeFilter, similar to ServiceFilter, but allows arguments to be passed as objects.
- Drawback: messy syntax, limited compiler checks.
4. Implement IFilterFactory and write your own factory to configure your filter.
- Now you have to unit test two things and have similar problem to the original Filter.
5. I've seen global filters suggested, but that seems like killing the patient in order to cure them...
6. Implement Middleware class that reads HttpContext.Items passed by your filter and acts accordingly.
- Intriguing option. Again, two items to unit test and the protocol is kind of adhoc.

## One Can Argue...

### Should we really be needing to inject service(s) into a Filter implementation? 

One assumes that you want fairly lightweight code in Filters, and the need to inject service(s) seems
to be a bit of a code smell. By definition services are reusable - and if you need to reuse the same code over-and-over again in your Filters, there's probably a design issue to address and re-factor anyway. Perhaps you really need a middleware component instead.

### OK, I really need (want) to inject a service in my scenario.

In that case, the above workarounds may provide a source of inspiration. Either way, you may have
more or less things to unit test.

## Unit Testing - setting up the plumbing

I've seen a lot of unit tests for various AspNet components that have an "arrange" section of several lines
of code just to construct the neccessary plumbing expected by the framework before you can actually
test the component (SUT). 

Our unit tests shouldn't be testing AspNet (stating the obvious) and having to setup the "plumbing" everytime
is a violation of DRY. Solution: TestHelper utilities, but everyone knows that.

The other impulse I have had, and probably shared by other developers, is to abstract and extract 
the custom logic needed into a POC without AspNet-specific dependencies. This is nice a clean and can lead to code portabiliy - but this leads to the problem of how to wire-up your new component - and in AspNet
your options are limited by the Filter API in particular.

## Setting up the plumbing...

So, whichever solution meets our needs, we invevitably are left with setting up "plumbing" for Filters.
To that end, I've shown in this tiny example project how to setup the plumbing for a simple Filter that
doesn't have any service dependencies. The Filter is trivial - just enough to have something working that 
can be shown in a unit test.

I may add working examples of the above DI alternatives to this project (an interesting exercise), but I still 
feel this scenario should be a rare exception rather than a rule.

